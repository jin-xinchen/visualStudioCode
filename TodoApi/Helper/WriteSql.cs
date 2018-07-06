using System;

namespace TodoApi.Helper
{
    class WriteSql
    {
        static void Start()
        {
            System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"D:\jinchen\projects\webapi2\20180705_enter_all_device_into_Devices_table_nt5.txt", true);

            while (true) // Loop indefinitely
            {
                Console.WriteLine("Enter input:"); // Prompt
                string line = Console.ReadLine(); // Get string from user
                if (line == "exit") // Check string
                {
                    break;
                }
                else
                {
                    file.WriteLine(line);
                    Console.Write("You typed "); // Report output
                    Console.Write(line.Length);
                    Console.WriteLine(" character(s)");
                }

            }
            file.Flush();
            file.Close();

            //  using (System.IO.StreamWriter file = 
            //             new System.IO.StreamWriter(@"D:\jinchen\projects\webapi2\20180705_enter_all_device_into_Devices_table_nt4.txt", true))
            //         {
            //             file.WriteLine("Fourth line");
            //         }
        }
    }
}