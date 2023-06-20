﻿using System.Diagnostics;

namespace butters
{
    class Program
    {
        public static bool verbose = false;
        public static bool building = false;
        public static bool timed = false;
        static Stopwatch stopwatch = new Stopwatch();

        public static void log(string msg){
            if(verbose){
                Console.WriteLine(msg);
            }
        }

        static void Main(string[] args){

            if(args.Contains("--b")){ building = true; verbose = true; }
            if(args.Contains("-v")){ verbose = true; }
            if(args.Contains("-t")){ timed = true; }

            Compile compiler = new Compile();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            log("args: " + String.Join(" | ", args));
            log("args length: " + args.Length);
            Console.ForegroundColor = ConsoleColor.White;
            if(args.Length >= 2){
                if(args[0] == "comp"){
                    if (timed)
                    {
                        stopwatch.Start();
                    }
                    compiler.comp(args[1]);
                    stopwatch.Stop();
                    if (timed)
                    {
                        Console.WriteLine($"compilation finished in {stopwatch.ElapsedMilliseconds} milliseconds.");
                    }
                }else if(args[0] == "build"){
                    throw new NotImplementedException();
                }else if(args[0] == "run"){
                    runtime run = new runtime(args[1]);
                    run.run(stopwatch);
                }else if(args[0] == "do"){
                    if (timed){stopwatch.Start();}
                    try
                    {
                        compiler.comp(args[1]);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine("something went wrong while compiling!");
                        log(e.ToString());
                        throw;
                    }
                    if(timed) Console.WriteLine($"finished compiling in {stopwatch.ElapsedMilliseconds} milliseconds.");
                    try
                    {
                        runtime runner = new runtime(compiler._latestMETA.project + ".bcomp");
                        runner.run(new Stopwatch());
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine("Something went wrong wile running");
                        log(e.ToString());
                        throw;
                    }

                    if (timed){
                        stopwatch.Stop();
                        Console.WriteLine($"finished everything in {stopwatch.ElapsedMilliseconds} milliseconds.");
                    }
                }else{
                    throw new Exception("Invalid arguements providided!");
                }
            }else{
                throw new Exception("Invalid arguements provided!");
            }

            if(building){
                Console.ForegroundColor = ConsoleColor.Cyan;
                log("build be don i think");    // it has to be else this wont run
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}