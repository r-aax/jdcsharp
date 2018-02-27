using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.IO;
using Lib.MathMod.Grid;
using Lib.MathMod.Grid.Partitioning;

namespace GridMasterConsole
{
    /// <summary>
    /// Console version of GridMaster.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Print help.
        /// </summary>
        private static void PrintHelp()
        {
            // Help.
            Console.WriteLine("Print help:");
            Console.WriteLine("./GridMasterConsole -h");
            Console.WriteLine("");

            // MCC grid partitioning.
            Console.WriteLine("Min cuts count blocks distribution (MCC-distr):");
            Console.WriteLine("./GridMasterConsole MCC-distr");
            Console.WriteLine("    load=<grid PFG or IBC file>");
            Console.WriteLine("    partitions=<partitions count, 1 by default>");
            Console.WriteLine("    min-cut-perc=<threshold for new block size after cutting, 10 by default>");
            Console.WriteLine("    save=<new grid filename, two files (PFG and IBC) will be created>");
            Console.WriteLine("");

            // GU grid partitioning.
            Console.WriteLine("Greedy uniform blocks distribution (GU-distr):");
            Console.WriteLine("./GridMasterConsole GU-distr");
            Console.WriteLine("    load=<grid PFG or IBC file>");
            Console.WriteLine("    partitions=<partitions count, 1 by default>");
            Console.WriteLine("    iters=<max iterations count, 10 by default>");
            Console.WriteLine("    dev=<deviation of partition weigh from mean value, 10 by default>");
            Console.WriteLine("    save=<new grid filename, two files (PFG and IBC) will be created");
        }

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                PrintHelp();
            }
            else if (args[0] == "MCC-distr")
            {
                Console.WriteLine("Min cuts count blocks distribution (MCC-distr):");

                File load = null;
                File load_pfg = null;
                File load_ibc = null;
                int partitions = 1;
                double min_cut_perc = 10.0;
                File save = null;
                File save_pfg = null;
                File save_ibc = null;

                for (int i = 1; i < args.Count(); i++)
                {
                    string[] ss = args[i].Split('=');

                    if (ss[0] == "load")
                    {
                        load = new File(ss[1]);

                        if (!load.ConsoleExistCheck())
                        {
                            return;
                        }

                        string load_ext = load.Ext.ToLower();

                        if ((load_ext != ".pfg") && (load_ext != ".ibc"))
                        {
                            Console.WriteLine(String.Format("error : unknown extension in the file {0}", load.Name));
                            Console.ReadKey();

                            return;
                        }

                        load_pfg = load.Copy();
                        load_ibc = load.Copy();

                        if (load_ext == ".pfg")
                        {
                            load_ibc.ChangeExtensionCaseSensitive(".ibc");
                        }
                        else
                        {
                            load_pfg.ChangeExtensionCaseSensitive(".pfg");
                        }

                        if (!load_pfg.ConsoleExistCheck() || !load_ibc.ConsoleExistCheck())
                        {
                            return;
                        }
                    }
                    else if (ss[0] == "partitions")
                    {
                        partitions = Int32.Parse(ss[1]);
                    }
                    else if (ss[0] == "min-cut-perc")
                    {
                        min_cut_perc = Double.Parse(ss[1]);
                    }
                    else if (ss[0] == "save")
                    {
                        save = new File(ss[1]);

                        if (load == null)
                        {
                            Console.WriteLine("error : load file must be defined before save file");
                            Console.ReadKey();

                            return;
                        }

                        if (load.IsLowerExt)
                        {
                            save_pfg = new File(save.Name + ".pfg");
                            save_ibc = new File(save.Name + ".ibc");
                        }
                        else
                        {
                            save_pfg = new File(save.Name + ".PFG");
                            save_ibc = new File(save.Name + ".IBC");
                        }
                    }
                    else
                    {
                        Console.WriteLine(String.Format("error : MCC-distr unknown parameter ({0})", args[i]));
                        Console.ReadKey();

                        return;
                    }
                }

                if ((load_pfg == null) || (load_ibc == null))
                {
                    Console.WriteLine(String.Format("error : no load file defined"));
                    Console.ReadKey();

                    return;
                }

                if ((save_pfg == null) || (save_ibc == null))
                {
                    Console.WriteLine(String.Format("error : no save file defined"));
                    Console.ReadKey();

                    return;
                }

                Console.WriteLine("Parsed string:");
                Console.WriteLine("./GridMasterConsole MCC-distr");
                Console.WriteLine(String.Format("    load_pfg=\"{0}\"", load_pfg.Name));
                Console.WriteLine(String.Format("    load_ibc=\"{0}\"", load_ibc.Name));
                Console.WriteLine(String.Format("    partitions={0}", partitions));
                Console.WriteLine(String.Format("    min-cut-perc={0}", min_cut_perc));
                Console.WriteLine(String.Format("    save_pfg=\"{0}\"", save_pfg.Name));
                Console.WriteLine(String.Format("    save_ibc=\"{0}\"", save_ibc.Name));
                Console.WriteLine("");

                Console.WriteLine("Process: started.");

                StructuredGrid Grid = null;



                // Partition.
                //MinimalCutsPartitioner partitioner = new MinimalCutsPartitioner(Grid);
                //int blocks_before = Grid.BlocksCount;
                //partitioner.Partition(partitions, min_cut);
                //int blocks_after = Grid.BlocksCount;
                //int cuts = blocks_after - blocks_before;

                // Upfdate information.
                //UpdateBriefGridStatistic();
                //LastCuts = cuts;
                //InitHistogramExt(partitions);
                //UpdateLastAction(String.Format("MCC distr: {0} cuts, {1}% deviation", cuts, Hist.Dev));

                Console.WriteLine("Process: finished");
            }
            else if (args[0] == "GU-distr")
            {
                Console.WriteLine("Greedy uniform blocks distribution (GU-distr):");
            }
            else if (args[0] == "-h")
            {
                PrintHelp();
            }
            else
            {
                Console.WriteLine(String.Format("error : unknown parameters ({0})", args[0]));
                Console.ReadKey();
          
                return;
            }

            Console.ReadKey();
        }
    }
}
