using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Threading;


namespace GeneralImportExport
{
    class EntityImportExport
    {
#region "Import"
        private static void ImportSingle(IEntitySet entitySetToCreateIn, string[] headers, string[] data)
        {
            // Create the new entity
            IEntityObject newEntity = entitySetToCreateIn.AddNew();

            Microsoft.LightSwitch.Details.IEntityProperty currentProperty;
            object newValue;

            // Loop through all propertyNames from the first line of the file
            for (int i = 0; i < headers.Count() ; i++)
            {
                try
                {
                    // Get the property from the new entity by name
                    currentProperty = newEntity.Details.Properties[headers[i]];
                }
                catch (ArgumentException)
                {

                    throw new InvalidOperationException(String.Format("A property named {0} does not exist on the entity named {1}.", headers[i], newEntity.Details.Name));
                }

                try
                {
                    // Convert the value
                    newValue = Convert.ChangeType(data[i], currentProperty.PropertyType, null);

                    currentProperty.Value = newValue;
                }
                catch (System.FormatException)
                {
                    throw new InvalidOperationException(String.Format("The following line has an invalid value for property {0}.  Aborting the import.\nData: {1}", headers[i], String.Join(",", data)));
                }
            }
        }


        private static void PerformImport(System.IO.FileInfo file, IEntitySet entitySetToCreateIn)
        {

            using (System.IO.StreamReader reader = file.OpenText())
            {
                string inputLine = reader.ReadLine();

                // Get the property names from the first line of input
                string[] headers = inputLine.Split(',').Select(header => header.Trim()).ToArray();
                int count = headers.Count();
                string[] data;

                inputLine = reader.ReadLine();
                while (inputLine != null)
                {
                    data = inputLine.Split(',');

                    // If the right number of data items were found, import the line
                    if (data.Count() == count)
                    {
                        ImportSingle(entitySetToCreateIn, headers, data);
                    }
                    else
                    {
                        throw new InvalidOperationException(String.Format("Line not imported.  Invalid number of elements.  Data = [{0}].", inputLine));
                    }
                    inputLine = reader.ReadLine();
                }
                reader.Close();
            }
            entitySetToCreateIn.Details.DataService.SaveChanges();
        }

        public void PromptAndImportEntities()
        {
            System.IO.FileInfo file = null;

            // OpenFileDialog() must be opened on the UI thread
            Dispatchers.Main.Invoke(() =>
                {
                    System.Windows.Controls.OpenFileDialog dlg = new System.Windows.Controls.OpenFileDialog();
                    dlg.Filter = "XML file (*.xml)|*.csv|Text Files (*.txt)|*.txt";

                    if (dlg.ShowDialog() == true)
                    {
                        file = dlg.File;
                    }
                });

           
        }
#endregion

#region "Export"
        private static void ExportSingle(System.IO.StreamWriter writer, IEntityObject entity, string[] properties)
        {
            List<string> stringArray = new List<string>();
            Microsoft.LightSwitch.Details.IEntityProperty currentProperty;

            // Write each property to the string array
            foreach (string prop in properties)
            {
                try
                {
                    // Get the property from the entity by name
                    currentProperty = entity.Details.Properties[prop];
                }
                catch (Exception)
                {
                    throw new InvalidOperationException(String.Format("A property named {0} does not exist on the entity named {1}.", prop, entity.Details.Name));
                }
                stringArray.Add(currentProperty.Value.ToString());
            }
            // Write the string array
            writer.WriteLine(String.Join(",", stringArray.ToArray()));
        }

        private static void PerformExport(System.IO.Stream file, IEnumerable entitiesToExport, string[] properties)
        {
            // Initialize a writer
            System.IO.StreamWriter writer = new System.IO.StreamWriter(file);
            writer.AutoFlush = true;

            // Write the header
            writer.WriteLine(String.Join(",", properties));

            // Export each entity separately
            foreach (IEntityObject entity in entitiesToExport)
            {
                ExportSingle(writer, entity, properties);   
            }
        }

        public static void PromptAndExportEntities(IEnumerable entitiesToExport, string[] properties)
        {
            //            Dim stream As IO.Stream = Nothing
            System.IO.Stream stream = null;

            // SaveFileDialog() must be opened on the UI thread
            Dispatchers.Main.Invoke(() =>
            {
                System.Windows.Controls.SaveFileDialog dlg = new System.Windows.Controls.SaveFileDialog();
                dlg.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";
                dlg.DefaultExt = "csv";

                if (dlg.ShowDialog() == true)
                {
                    stream = dlg.OpenFile();
                }
            });

            if (stream != null)
            {
                PerformExport(stream, entitiesToExport, properties);

                // Need to close the file on the UI thread as well
                Dispatchers.Main.Invoke(() =>
                {
                    stream.Close();
                });

            }
        }
#endregion
    }
}

