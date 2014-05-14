using Microsoft.LightSwitch;

using Microsoft.LightSwitch.Framework;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.IO;


namespace LightSwitchApplication.UserCode
{

    class ExportToWord
    {

        /// <summary>

        /// Create a mailing label document based upon our Contacts and their corresponding address information

        /// </summary>

        /// <returns>A byte array containing the generated Word Mailing Label Document</returns>

        public static byte[] ExportToMailingLabelDocumentWithCOM()
        {

            Object fileName = String.Concat(

                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),

                @"\Partners_" + System.DateTime.UtcNow.ToFileTimeUtc() + ".docx");

            Object labelName = "8660 Easy Peel Address Labels";


            dynamic wordObject = System.Runtime.InteropServices.Automation.AutomationFactory.CreateObject("Word.Application");

            wordObject.Documents.Add();


            // Create an empty mailing label document

            dynamic mailingLabelDoc = wordObject.MailingLabel.CreateNewDocument(ref labelName);

            try
            {

                // Format the contacts address information

                List<string> contacts = FormatContactsAddressInformation();


                // Populate the word document with labels

                PopulateLabels(contacts, mailingLabelDoc);


                // Save our label doc as the default word format (i.e. docx)

                mailingLabelDoc.SaveAs2(ref fileName);

            }


            catch (Exception e)
            {

                throw new InvalidOperationException("Failed to create document", e);

            }


            finally
            {

                // Wait for Word to shut down to ensure the document is released

                if (mailingLabelDoc != null)
                {

                    mailingLabelDoc.Close();

                }

                if (wordObject != null)
                {

                    wordObject.Quit();

                }


                // Cast our wordObject as IDisposable so that we can 

                // invoke .Dispose() and be sure the COM object is released

                IDisposable disposable = wordObject as IDisposable;

                if (disposable != null)
                {

                    disposable.Dispose();

                }

            }


            // Take our newly created word document, and stick into our a Byte[] array

            byte[] buffer = File.ReadAllBytes((string)fileName);


            // Delete the file now that we are done with it

            // We'll pass back the byte[] buffer and let the user choose a filename and location

            File.Delete((string)fileName);


            return buffer;

        }


        /// <summary>

        /// Formats each contacts address information and stores the data into a List

        /// </summary>

        private static List<string> FormatContactsAddressInformation()
        {

            // Populate a List with our contacts and their address information

            List<string> contacts = new List<string>();


            EntitySet<Contact> contactsSet = Application.Current.CreateDataWorkspace().ApplicationData.Contacts;

            foreach (Contact contact in contactsSet.OrderBy(x => x.LastName))
            {

                string formattedAddress = contact.FirstName + " " + contact.LastName + Environment.NewLine + contact.AddressLine1 + Environment.NewLine;

                if (!String.IsNullOrEmpty(contact.AddressLine2))
                {

                    formattedAddress += contact.AddressLine2 + Environment.NewLine;

                }


                formattedAddress += contact.City + " " + contact.State + " " + contact.ZipCode + Environment.NewLine;


                if (!String.IsNullOrEmpty(contact.Country))
                {

                    formattedAddress += contact.Country;

                }


                contacts.Add(formattedAddress);

            }


            return contacts;

        }


        /// <summary>

        /// Populate the Labels in a given word document with our Contact's information

        /// </summary>

        private static void PopulateLabels(List<string> contacts, dynamic wordDocument)
        {

            dynamic docTable = wordDocument.Tables[1];


            // Starting at row 1, iterate through each row and column in the Mailing Label's Table

            // And for each cell, insert our Contact's address information

            int row = 1;

            int currentContactIndex = 0;

            while (currentContactIndex < contacts.Count)
            {

                // Skip even numbered columns since they are "divider" columns and not intended for cell text

                for (int column = 1; column <= docTable.Columns.Count; column += 2)
                {

                    // If we've run out of Contacts, break out

                    if (currentContactIndex >= contacts.Count)
                    {

                        break;

                    }

                    docTable.Cell(row, column).Range.Text = contacts[currentContactIndex];

                    docTable.Cell(row, column).Range.ParagraphFormat.Alignment = 1;

                    docTable.Cell(row, column).Range.Cells.VerticalAlignment = 1;

                    docTable.Cell(row, column).Range.Rows.Alignment = 1;

                    currentContactIndex++;

                }

                // Keep track of what row we are on in our table

                row++;


                // Check to see if we've run out of rows in our table

                if (row > docTable.Rows.Count)
                {

                    // Add a new row to our table

                    docTable.Rows.Add();

                }

            }

        }

    }

}

