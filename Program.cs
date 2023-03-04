using Microsoft.Win32;


namespace Sweet32Fixer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This application will assess Seet32 vulnerability in your windows machine");

            while(true)
            {
                // Console.WriteLine("Press Enter to continue ..."); Console.ReadLine();
                Console.WriteLine("\nTo Asses the cipher suits, Press 'A'");
                Console.WriteLine("To Fix the weak cipher suits, Press 'F', it may not work on your system :{");
                Console.WriteLine("To Exit: press Escape");
                var userinput = Console.ReadKey().Key;

                if(userinput == ConsoleKey.Escape)
                    Environment.Exit(0);

                else if (userinput == ConsoleKey.A )
                {
                    try
                    {
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Cryptography\\Configuration\\Local\\SSL\\00010002"))
                        {
                            if (key != null)
                            {
                                string[] obj = (string[])key.GetValue("Functions");

                                if (obj != null)
                                {
                                    foreach(string str in obj)
                                    {
                                        if(str.Contains("3DES") || str.Contains("RC4"))
                                            Console.WriteLine("Weak cipher found: " + str);
                                    }
                                }
                            }
                            else
                                Console.WriteLine("The desired key does not exist on this machine");
                        }
                    }
                    catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                        //react appropriately
                    }
                }
                else if(userinput == ConsoleKey.F)
                {
                    try
                    {
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Cryptography\\Configuration\\Local\\SSL\\00010002"))
                        {
                            if (key != null)
                            {
                                // string[] obj = (string[])key.GetValue("Functions");
                                List<string> obj = ((string[])key.GetValue("Functions")).ToList();

                                if (obj != null)
                                {
                                    foreach(string str in obj)
                                    {
                                        if(str.Contains("3DES") || str.Contains("RC4"))
                                            {obj.Remove(str);Console.WriteLine(str + " cipher has been removed from this machine");}

                                    }
                                    key.SetValue("Function",obj);
                                }

                            }
                            else
                                Console.WriteLine("The desired key does not exist on this machine");
                        }
                    }
                    catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                        //react appropriately
                    }
                }            
            }
        }
    }

}