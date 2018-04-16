using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using System.IO;
using System.Diagnostics;

namespace ExcelToJson
{
    class ExcelToJson_Main
    {
        static void Main(string[] args)
        {
            try
            {
                //Set the output directory to the Json folder in the base directory
                Utils.OutputDir = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}Json");

                //Obtain the source data xlsx file
                DirectoryInfo BaseDirectory = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}");
                DirectoryInfo SourceDataDir = new DirectoryInfo(BaseDirectory.Parent.Parent.FullName + "\\xlsx");
                FileInfo SourceDataFile = new FileInfo(SourceDataDir.FullName + "\\JSONPayloads.xlsx");


                using (ExcelPackage package = new ExcelPackage(SourceDataFile))
                {

                   
                    //intialize variables
                    var JsonSheetsQuery = (from s in package.Workbook.Worksheets where s.Name.EndsWith(".json") select s);
                    int count = 0;

                    //Generate json for each worksheet that ends with ".json"
                    foreach (var s in JsonSheetsQuery)
                    {
                        var JsonQuery = (from cell in s.Cells["a:a:"] select cell);

                        //Create json file to write
                        string outputJsonPath = Utils.OutputDir.FullName + "\\" + s.Name ;
                        Console.WriteLine($"Json output file name is {outputJsonPath}");
                        FileInfo outputJson = new FileInfo(outputJsonPath);

                        //Write json output
                        using (StreamWriter sw = outputJson.CreateText())
                        {
                            count = 0;
                            foreach (var cell in JsonQuery)
                            {
                                //Always skip the header row
                                //TODO: configure whether to skip header
                                if (count > 0)
                                {
                                    sw.WriteLine("{0}", cell.Value);
                                }
                                count++;

                            }

                            sw.Dispose();
                        }
                        Console.WriteLine("{0} rows found in sheet {1}...", count-1,s.Name);
                        Console.WriteLine();
                        
                    }




                    package.Dispose();
                }



                Console.WriteLine($"Source data file is {SourceDataFile.FullName}");

                var prevColor = Console.ForegroundColor;
                Console.WriteLine($"Generated json files can be found in {Utils.OutputDir.FullName}");
                Process.Start(Utils.OutputDir.FullName);

                Console.WriteLine();
                Console.WriteLine("Press the return key to exit...");
                Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.Read();
            }

        }


    }
}
