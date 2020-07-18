using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileVisitor
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Enter path for searching:");
      var path = Console.ReadLine();

      Predicate<string> predicate = (info) => info.Contains("txt");

      FileSystemVisitor fsVisitor;

      try
      {
        fsVisitor = new FileSystemVisitor(path, predicate);
      }
      catch (DirectoryNotFoundException e)
      {
        throw new DirectoryNotFoundException("Invalid directory path.", e);
      }
      catch (ArgumentNullException e)
      {
        throw new ArgumentNullException("Invalid argument.", e);
      }
      catch (Exception)
      {
        throw;
      }

      fsVisitor.SearchStarted += (sender, e) => { Console.WriteLine("[START]"); };
      fsVisitor.SearchFinished += (sender, e) => { Console.WriteLine("[FINISHED]"); };

      fsVisitor.DirectoryFound += (sender, e) => { Console.WriteLine($"Directory: {e.Path}"); };
      fsVisitor.FileFound += (sender, e) => { Console.WriteLine($"File: {e.Path}"); };

      fsVisitor.FilteredDirectoryFound += (sender, e) => { Console.WriteLine($"Filtered Directory: {e.Path}"); };
      fsVisitor.FilteredFileFound += (sender, e) => { Console.WriteLine($"Filtered File: {e.Path}"); };

      var result = new List<string>();

      try
      {
        foreach (string item in fsVisitor)
        {
          result.Add(item);
        }
      }
      catch (DirectoryNotFoundException e)
      {
        throw new DirectoryNotFoundException("Invalid directory path.", e);
      }
      catch (Exception)
      {
        throw;
      }

      Console.WriteLine("Results:");

      foreach (var item in result)
      {
        Console.WriteLine(item);
      }

      Console.ReadLine();
    }
  }
}
