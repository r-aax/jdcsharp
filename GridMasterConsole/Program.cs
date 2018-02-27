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
        /// Load file.
        /// </summary>
        private static File Load;

        /// <summary>
        /// Load PFG file.
        /// </summary>
        private static File LoadPFG;

        /// <summary>
        /// Load IBC File.
        /// </summary>
        private static File LoadIBC;

        /// <summary>
        /// Save file.
        /// </summary>
        private static File Save;

        /// <summary>
        ///  Save PFG file.
        /// </summary>
        private static File SavePFG;

        /// <summary>
        /// Save IBC file.
        /// </summary>
        private static File SaveIBC;

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
        /// Define load files.
        /// </summary>
        /// <param name="val">string value from which we try to extract load files names</param>
        /// <returns><c>true</c> - if load files are defined, <c>false</c> - otherwise</returns>
        private static bool DefineLoadFiles(string val)
        {
            Load = new File(val);

            if (!Load.ConsoleExistCheck())
            {
                return false;
            }

            string load_ext = Load.Ext.ToLower();

            if ((load_ext != ".pfg") && (load_ext != ".ibc"))
            {
                Console.WriteLine(String.Format("error : unknown extension in the file {0}", Load.Name));

                return false;
            }

            LoadPFG = Load.Copy();
            LoadIBC = Load.Copy();

            if (load_ext == ".pfg")
            {
                LoadIBC.ChangeExtensionCaseSensitive(".ibc");
            }
            else
            {
                LoadPFG.ChangeExtensionCaseSensitive(".pfg");
            }

            if (!LoadPFG.ConsoleExistCheck() || !LoadIBC.ConsoleExistCheck())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Define save files from string value.
        /// </summary>
        /// <param name="val">string value</param>
        /// <returns><c>true</c> - if save files are defined, <c>false</c> - otherwise</returns>
        private static bool DefineSaveFiles(string val)
        {
            Save = new File(val);

            if (Load == null)
            {
                Console.WriteLine("error : load file must be defined before save file");

                return false;
            }

            if (Load.IsLowerExt)
            {
                SavePFG = new File(Save.Name + ".pfg");
                SaveIBC = new File(Save.Name + ".ibc");
            }
            else
            {
                SavePFG = new File(Save.Name + ".PFG");
                SaveIBC = new File(Save.Name + ".IBC");
            }

            return true;
        }

        /// <summary>
        /// Check if load and save files defined.
        /// </summary>
        /// <param name="load_pfg">load PFG file</param>
        /// <param name="load_ibc">load IBC file</param>
        /// <param name="save_pfg">save PFG file</param>
        /// <param name="save_ibc">save IBC file</param>
        /// <returns><c>true</c> - if load and save files are defines, <c>false</c> - otherwise</returns>
        private static bool IsLoadSaveFilesDefined(File load_pfg, File load_ibc,
                                                   File save_pfg, File save_ibc)
        {
            bool res = true;

            if ((load_pfg == null) || (load_ibc == null))
            {
                Console.WriteLine(String.Format("error : no load file defined"));

                res = false;
            }

            if ((save_pfg == null) || (save_ibc == null))
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
            int partitions = 1;
            int min_margin = 1;

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

                double min_cut_perc = 10.0;

                for (int i = 1; i < args.Count(); i++)
                {
                    string[] ss = args[i].Split('=');

                    if (ss[0] == "load")
                    {
                        if (!DefineLoadFiles(ss[1]))
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
                        if (!DefineSaveFiles(ss[1]))
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine(String.Format("error : MCC-distr unknown parameter ({0})", args[i]));

                        return 1;
                    }
                }

                // Check load and save files.
                if (!IsLoadSaveFilesDefined(LoadPFG, LoadIBC, SavePFG, SaveIBC))
                {
                    return 1;
                }

                // Print parsed string.
                Console.WriteLine("Parsed string:");
                Console.WriteLine("GridMasterConsole.exe MCC-distr");
                Console.WriteLine(String.Format("    load_pfg=\"{0}\"", LoadPFG.Name));
                Console.WriteLine(String.Format("    load_ibc=\"{0}\"", LoadIBC.Name));
                Console.WriteLine(String.Format("    partitions={0}", partitions));
                Console.WriteLine(String.Format("    min-cut-perc={0}", min_cut_perc));
                Console.WriteLine(String.Format("    min-margin={0}", min_margin));
                Console.WriteLine(String.Format("    save_pfg=\"{0}\"", SavePFG.Name));
                Console.WriteLine(String.Format("    save_ibc=\"{0}\"", SaveIBC.Name));
                Console.WriteLine("");

                Console.WriteLine("Process: started.");

                StructuredGrid grid = new StructuredGrid();

                // Grid load.
                if (GridLoaderSaverPFG.Load(grid, LoadPFG.Name, LoadIBC.Name,
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
                GridLoaderSaverPFG.Save(grid, SavePFG.Name, SaveIBC.Name);

                Console.WriteLine("Process: finished.");
            }
            else if (args[0] == "GU-distr")
            {
                Console.WriteLine("Greedy uniform blocks distribution (GU-distr):");

                int iters = 10;
                double dev = 10.0;

                for (int i = 1; i < args.Count(); i++)
                {
                    string[] ss = args[i].Split('=');

                    if (ss[0] == "load")
                    {
                        if (!DefineLoadFiles(ss[1]))
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
                        if (!DefineSaveFiles(ss[1]))
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine(String.Format("error : GU-distr unknown parameter ({0})", args[i]));

                        return 1;
                    }
                }

                // Check load and save files.
                if (!IsLoadSaveFilesDefined(LoadPFG, LoadIBC, SavePFG, SaveIBC))
                {
                    return 1;
                }

                // Print parsed string.
                Console.WriteLine("Parsed string:");
                Console.WriteLine("GridMasterConsole.exe GU-distr");
                Console.WriteLine(String.Format("    load_pfg=\"{0}\"", LoadPFG.Name));
                Console.WriteLine(String.Format("    load_ibc=\"{0}\"", LoadIBC.Name));
                Console.WriteLine(String.Format("    partitions={0}", partitions));
                Console.WriteLine(String.Format("    iters={0}", iters));
                Console.WriteLine(String.Format("    dev={0}", dev));
                Console.WriteLine(String.Format("    min-margin={0}", min_margin));
                Console.WriteLine(String.Format("    save_pfg=\"{0}\"", SavePFG.Name));
                Console.WriteLine(String.Format("    save_ibc=\"{0}\"", SaveIBC.Name));
                Console.WriteLine("");

                Console.WriteLine("Process: started.");

                StructuredGrid grid = new StructuredGrid();

                // Grid load.
                if (GridLoaderSaverPFG.Load(grid, LoadPFG.Name, LoadIBC.Name,
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
                GridLoaderSaverPFG.Save(grid, SavePFG.Name, SaveIBC.Name);

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
