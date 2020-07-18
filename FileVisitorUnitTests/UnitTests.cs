using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleFileVisitor;
using System.Collections.Generic;

namespace FileVisitorUnitTests
{
  [TestClass]
  public class UnitTests
  {
    [TestMethod]
    public void FileVisitorCheckResultWithPredicateTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor";

      bool predicate(string info) => info.Contains("jpg");

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path, predicate);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreEqual(result.Count, 1);

    }

    [TestMethod]
    public void FileVisitorCheckResultWitoutPredicateTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor";

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

        Assert.AreNotEqual(result.Count, 1);
      }

    [TestMethod]
    public void FileVisitorCheckResultWitoutPredicateCountTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor/ConsoleFileVisitor";

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreEqual(result.Count, 22);
    }

    [TestMethod]
    public void FileVisitorNotFoundTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor/ConsoleFileVisitor";

      bool predicate(string info) => info.Contains("jpg");

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path, predicate);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    public void FileVisitorCheckResultForFileNameTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor";

      Predicate<string> predicate = (info) => info.Contains("ConsoleFileVisitor");

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path, predicate);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreNotEqual(result.Count, 1);
      
    }

    [TestMethod]
    public void FileVisitorCheckResultForTextFileTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor";

      bool predicate(string info) => info.Contains("txt");

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path, predicate);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreEqual(result.Contains("C:/Users/aziza/source/repos/ConsoleFileVisitor\\1.txt"), true);

    }


    [TestMethod]
    public void FileVisitorCheckResultForTextFilesCountTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor";

      bool predicate(string info) => info.Contains("txt");

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path, predicate);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreEqual(result.Count, 5);

    }

    [TestMethod]
    public void FileVisitorCheckResultForCsFilesCountTest()
    {
      var path = "C:/Users/aziza/source/repos/ConsoleFileVisitor";

      bool predicate(string info) => info.Contains("cs");

      FileSystemVisitor fsVisitor;

      fsVisitor = new FileSystemVisitor(path, predicate);

      var result = new List<string>();

      foreach (string item in fsVisitor)
      {
        result.Add(item);
      }

      Assert.AreNotEqual(result.Count, 1);

    }

  }

}
 

