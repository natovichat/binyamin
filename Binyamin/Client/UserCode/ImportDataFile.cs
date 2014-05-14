//' Copyright © Microsoft Corporation.  All Rights Reserved.

//' This code released under the terms of the 

//' Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html)

using System;

using System.Collections.Generic;

using System.IO;


namespace LightSwitchApplication.UserCode
{

    public class ImportDataFile
    {

        /// <summary>

        /// Import data from a comma delimited stream

        /// And using the workspace that was passed in, insert the imported data

        /// so it displays on the user's screen

        /// </summary>

        static public void ImportCommaDelimitedStream(FileStream fileStream, DataWorkspace dataWorkspace)
        {

            List<string[]> parsedData = new List<string[]>();


            using (StreamReader streamReader = new StreamReader(fileStream))
            {

                string line;

                string[] row;


                while ((line = streamReader.ReadLine()) != null)
                {

                    row = line.Split(',');

                    parsedData.Add(row);

                }

            }

            AddData(parsedData, dataWorkspace);

        }


        /// <summary>

        /// Take in a list of string arrays which contains

        /// all the parsed data for our Contacts entity

        /// </summary>

        static private void AddData(List<string[]> dataList, DataWorkspace dataWorkspace)
        {

            foreach (string[] row in dataList)
            {

                Contact contact = dataWorkspace.ApplicationData.Contacts.AddNew();


                contact.FirstName = row[0];

                contact.MiddleName = row[1];

                contact.LastName = row[2];

                contact.AddressLine1 = row[3];

                contact.AddressLine2 = row[4];

                contact.City = row[5];

                contact.State = row[6];

                contact.ZipCode = row[7];

                contact.Country = row[8];

                contact.PhoneNumber1 = row[9];

                contact.PhoneNumber2 = row[10];

                contact.BirthDate = DateTime.Parse(row[11]);

                contact.MiscellaneousInformation = row[12];

            }

        }

    }

}

