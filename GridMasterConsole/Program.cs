using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.IO;
using Lib.Utils;
using Lib.MathMod.Grid;
using Lib.MathMod.Grid.Cut;
using Lib.MathMod.Grid.Load;
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
            Console.WriteLine("GridMasterConsole.exe -h");
            Console.WriteLine("");

            // MCC grid partitioning.
            Console.WriteLine("Min cuts count blocks distribution (MCC-distr):");
            Console.WriteLine("GridMasterConsole.exe MCC-distr");
            Console.WriteLine("    load=<grid PFG or IBC file>");
            Console.WriteLine("    partitions=<partitions count, 1 by default>");
            Console.WriteLine("    min-cut-perc=<threshold for new block size after cutting, 10 by default>");
            Console.WriteLine("    min-margin=<min margin value for cut, 1 by default>");
            Console.WriteLine("    save=<new grid filename, two files (PFG and IBC) will be created>");
            Console.WriteLine("");

            // GU grid partitioning.
            Console.WriteLine("Greedy uniform blocks distribution (GU-distr):");
            Console.WriteLine("GridMasterConsole.exe GU-distr");
            Console.WriteLine("    load=<grid PFG or IBC file>");
            Console.WriteLine("    partitions=<partitions count, 1 by default>");
            Console.WriteLine("    iters=<max iterations count, 10 by default>");
            Console.WriteLine("    dev=<deviation of partition weigh from mean value, 10 by default>");
            Console.WriteLine("    min-margin=<min margin value for cut, 1 by default>");
            Console.WriteLine("    save=<new grid filename, two files (PFG and IBC) will be created");
        }

        /// <summary>
        /// Check if load and save files defined.
        /// </summary>
        /// <param name="lpfg">load PFG file</param>
        /// <param name="libc">load IBC file</param>
        /// <param name="spfg">save PFG file</param>
        /// <param name="sibc">save IBC file</param>
        /// <returns><c>true</c> - if load and save files are defines, <c>false</c> - otherwise</returns>
        private static bool IsLoadSaveFilesDefined(File lpfg, File libc, File spfg, File sibc)
        {
            bool res = true;

            if ((lpfg == null) || (libc == null))
            {
                Console.WriteLine(String.Format("error : no load file defined"));

                res = false;
            }

            if ((spfg == null) || (sibc == null))
            {
                Console.WriteLine(String.Format("error : no save file defined"));

                res = false;
            }

            return res;
        }

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>0 - in case of successfull execution, 1 - if some error occured</returns>
        static int Main(string[] args)
        {
            if (args.Count() == 0)
            {
                PrintHelp();

                return 0;
            }

            if (args[0] == "-h")
            {
                PrintHelp();

                return 0;
            }

            if (args[0] == "MCC-distr")
            {
                Console.WriteLine("Min cuts count blocks distribution (MCC-distr):");

                File load = null;
                File load_pfg = null;
                File load_ibc = null;
                int partitions = 1;
                double min_cut_perc = 10.0;
                int min_margin = 1;
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
                            return 1;
                        }

                        string load_ext = load.Ext.ToLower();

                        if ((load_ext != ".pfg") && (load_ext != ".ibc"))
                        {
                            Console.WriteLine(String.Format("error : unknown extension in the file {0}", load.Name));

                            return 1;
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
                            return 1;
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
                    else if (ss[0] == "min-margin")
                    {
                        min_margin = Int32.Parse(ss[1]);
                    }
                    else if (ss[0] == "save")
                    {
                        save = new File(ss[1]);

                        if (load == null)
                        {
                            Console.WriteLine("error : load file must be defined before save file");

                            return 1;
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

                        return 1;
                    }
                }

                // Check load and save files.
                if (!IsLoadSaveFilesDefined(load_pfg, load_ibc, save_pfg, save_ibc))
                {
                    return 1;
                }

                // Print parsed string.
                Console.WriteLine("Parsed string:");
                Console.WriteLine("GridMasterConsole.exe MCC-distr");
                Console.WriteLine(String.Format("    load_pfg=\"{0}\"", load_pfg.Name));
                Console.WriteLine(String.Format("    load_ibc=\"{0}\"", load_ibc.Name));
                Console.WriteLine(String.Format("    partitions={0}", partitions));
                Console.WriteLine(String.Format("    min-cut-perc={0}", min_cut_perc));
                Console.WriteLine(String.Format("    min-margin={0}", min_margin));
                Console.WriteLine(String.Format("    save_pfg=\"{0}\"", save_pfg.Name));
                Console.WriteLine(String.Format("    save_ibc=\"{0}\"", save_ibc.Name));
                Console.WriteLine("");

                Console.WriteLine("Process: started.");

                StructuredGrid grid = new StructuredGrid();

                // Grid load.
                if (GridLoaderSaverPFG.Load(grid, load_pfg.Name, load_ibc.Name,
                                            GridLoadSavePFGProperties.EpsForBCondsMatchParallelMove,
                                            GridLoadSavePFGProperties.EpsForBCondsMatchRotation))
                {
                    Console.WriteLine("Process: the grid is loaded");
                }
                else
                {
                    Console.WriteLine(String.Format("error : grid loading fault"));

                    return 1;
                }

                // Partition.
                GridCutter.MinMargin = min_margin;
                MinimalCutsPartitioner partitioner = new MinimalCutsPartitioner(grid);
                int blocks_before = grid.BlocksCount;
                partitioner.Partition(partitions, min_cut_perc / 100.0);
                int blocks_after = grid.BlocksCount;
                int cuts = blocks_after - blocks_before;

                // Update information.
                HistogramExt hist = new HistogramExt(partitions, grid);
                Console.WriteLine(String.Format("Process: MCC distribution is done : {0} cuts, {1}% deviation", cuts, hist.Dev));

                // Grid save.
                GridLoaderSaverPFG.Save(grid, save_pfg.Name, save_ibc.Name);

                Console.WriteLine("Process: finished.");
            }
            else if (args[0] == "GU-distr")
            {
                Console.WriteLine("Greedy uniform blocks distribution (GU-distr):");

                File load = null;
                File load_pfg = null;
                File load_ibc = null;
                int partitions = 1;
                int iters = 10;
                double dev = 10.0;
                int min_margin = 1;
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
                            return 1;
                        }

                        string load_ext = load.Ext.ToLower();

                        if ((load_ext != ".pfg") && (load_ext != ".ibc"))
                        {
                            Console.WriteLine(String.Format("error : unknown extension in the file {0}", load.Name));

                            return 1;
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
                            return 1;
                        }
                    }
                    else if (ss[0] == "partitions")
                    {
                        partitions = Int32.Parse(ss[1]);
                    }
                    else if (ss[0] == "iters")
                    {
                        iters = Int32.Parse(ss[1]);
                    }
                    else if (ss[0] == "dev")
                    {
                        dev = Double.Parse(ss[1]);
                    }
                    else if (ss[0] == "min-margin")
                    {
                        min_margin = Int32.Parse(ss[1]);
                    }
                    else if (ss[0] == "save")
                    {
                        save = new File(ss[1]);

                        if (load == null)
                        {
                            Console.WriteLine("error : load file must be defined before save file");

                            return 1;
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
                        Console.WriteLine(String.Format("error : GU-distr unknown parameter ({0})", args[i]));

                        return 1;
                    }
                }

                // Check load and save files.
                if (!IsLoadSaveFilesDefined(load_pfg, load_ibc, save_pfg, save_ibc))
                {
                    return 1;
                }

                // Print parsed string.
                Console.WriteLine("Parsed string:");
                Console.WriteLine("GridMasterConsole.exe GU-distr");
                Console.WriteLine(String.Format("    load_pfg=\"{0}\"", load_pfg.Name));
                Console.WriteLine(String.Format("    load_ibc=\"{0}\"", load_ibc.Name));
                Console.WriteLine(String.Format("    partitions={0}", partitions));
                Console.WriteLine(String.Format("    iters={0}", iters));
                Console.WriteLine(String.Format("    dev={0}", dev));
                Console.WriteLine(String.Format("    min-margin={0}", min_margin));
                Console.WriteLine(String.Format("    save_pfg=\"{0}\"", save_pfg.Name));
                Console.WriteLine(String.Format("    save_ibc=\"{0}\"", save_ibc.Name));
                Console.WriteLine("");

                Console.WriteLine("Process: started.");

                StructuredGrid grid = new StructuredGrid();

                // Grid load.
                if (GridLoaderSaverPFG.Load(grid, load_pfg.Name, load_ibc.Name,
                                            GridLoadSavePFGProperties.EpsForBCondsMatchParallelMove,
                                            GridLoadSavePFGProperties.EpsForBCondsMatchRotation))
                {
                    Console.WriteLine("Process: the grid is loaded");
                }
                else
                {
                    Console.WriteLine(String.Format("error : grid loading fault"));

                    return 1;
                }

                // Partition.
                GridCutter.MinMargin = min_margin;
                GreedyUniformPartitioner partitioner = new GreedyUniformPartitioner(grid);
                int blocks_before = grid.BlocksCount;
                partitioner.Partition(partitions, iters, dev / 100.0);
                int blocks_after = grid.BlocksCount;
                int cuts = blocks_after - blocks_before;

                // Update information.
                HistogramExt hist = new HistogramExt(partitions, grid);
                Console.WriteLine(String.Format("Process: GU distribution is done : {0} cuts, {1}% deviation", cuts, hist.Dev));

                // Grid save.
                GridLoaderSaverPFG.Save(grid, save_pfg.Name, save_ibc.Name);

                Console.WriteLine("Process: finished.");
            }
            else
            {
                Console.WriteLine(String.Format("error : unknown parameters ({0})", args[0]));
          
                return 1;
            }

            return 0;
        }
    }
}
