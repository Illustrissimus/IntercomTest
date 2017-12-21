using System;
using System.Linq;
using System.Text;
using System.IO;
using IntercomTest;
using IntercomTest.Readers;
using System.Security;

namespace IntercomConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (string.IsNullOrWhiteSpace(args[0]))
                    PrintHelp();
                Console.WriteLine();
                Console.WriteLine("Closing the program. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            if (args.Length > 1)
            {
                PrintHelp();
                Console.WriteLine();
                Console.WriteLine("Closing the program. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            try
            {
                var dublinOfficeLocation = GeographicalLocation.FromDegrees(
                    ConfigurationReader.DublinOfficeDegreeLongitude, ConfigurationReader.DublinOfficeDegreeLatitude);

                var reader = CreateReader();
                var customers1 = reader.ReadCustomers();

                var customers2 = customers1?.Where(customer => dublinOfficeLocation.DistanceFrom(customer.Location) < ConfigurationReader.InvitationDistanceKilometers);
                var customers = customers2?.OrderBy(customer => customer.UserId);

                if (ReferenceEquals(customers, null))
                {
                    Console.WriteLine("An error has occurred! Customer info was note read.");
                    return;
                }

                Console.WriteLine("Customers to invite:");
                foreach(var customer in customers)
                {
                    Console.WriteLine("Name: {0}, user ID: {1}, distance: {2:0.00} km", customer.Name, customer.UserId,
                        customer.Location.DistanceFrom(dublinOfficeLocation));
                }
            }
            catch(IntercomTestException ite)
            {
                Console.WriteLine(string.Format("An error has ocurred while trying to read user information! Detailed information: {0}",
                    ite.IntercomTestErrorMessage));
                if (ite.HasInnerException)
                    Console.WriteLine("Inner exception: " + ite.InnerException.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("An internal erro has occurred!");
                Console.WriteLine(ane.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine("An internal erro has occurred!");
                Console.WriteLine(ae.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine("Customer file directory not found!");
                Console.WriteLine(dnfe.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("Customer file not found!");
                Console.WriteLine(fnfe.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (PathTooLongException ptle)
            {
                Console.WriteLine("Customer file path is too long!");
                Console.WriteLine(ptle.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("The current user does not have the perrmission to access the customer file" +
                    "or the operation is not supported beacuse the file is read-only!");
                Console.WriteLine(uae.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (NotSupportedException nse)
            {
                Console.WriteLine("Reading the customer file is not supported!. This may happen path is in invalid format.");
                Console.WriteLine(nse.Message);
                Console.WriteLine();
                PrintHelp();
            }
            catch (SecurityException se)
            {
                Console.WriteLine("The current user does not have the perrmission to access the customer file!");
                Console.WriteLine(se.Message);
                Console.WriteLine();
                PrintHelp();
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Closing the program. Press any key to continue...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Creates JSON text reader for the file.
        /// </summary>
        /// <returns></returns>
        static ICustomerReader CreateReader()
        {
            return ReaderFactory.CreateJsonTextReader(ConfigurationReader.CustomerFilePath);
        }

        /// <summary>
        /// Prints help to the constole.
        /// </summary>
        static void PrintHelp()
        {
            var builder = new StringBuilder();
            builder.AppendLine("To run the program, execute the command or double-click the .exe file.")
                .Append("By default, the program will try to read the customer list from the customers.txt file in the working directory.")
                .Append(" ").
                Append("If you want to change this, you can edit the line <add key=\"cutomerfile\" value=\"./ customers.txt\" />.")
                .Append(" ").
                Append("The file path is changed by changing the value in that line. If the line is deleted, the program will default to ./customers.txt");
        }
    }
}
