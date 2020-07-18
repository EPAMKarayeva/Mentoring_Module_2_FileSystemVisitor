using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileVisitor
{
  public class FileSystemVisitor : IEnumerable<string>
  {
    private readonly string dirRoot;
    private readonly Predicate<string> _filter = (x) => true;

    public FileSystemVisitor(string root)
    {
      if (root == null)
      {
        throw new ArgumentNullException(nameof(root));
      }
      if (!Directory.Exists(root))
      {
        throw new DirectoryNotFoundException();
      }
      dirRoot = root;
    }

    public FileSystemVisitor(string root, Predicate<string> filter) : this(root)
    {
      _filter = filter ?? throw new ArgumentNullException(nameof(filter));
    }

    private IEnumerable<string> SearchInFileSystem(string root)
    {
      if (root == null)
      {
        throw new ArgumentNullException();
      }

      OnSearchStarted(new EventArgs());

      foreach (var item in SearchInDirectory(root))
      {
        yield return item;
      }

     OnSearchFinished(new EventArgs());
    }

    private IEnumerable<string> SearchInDirectory(string root)
    {
      foreach (var file in SearchInFiles(root))
      {
        yield return file;
      }

      foreach (var subDirectory in Directory.EnumerateDirectories(root))
      {
        var directoryEventArgs = new DirectoryEventArgs { Path = subDirectory };
        OnDirectoryFound(directoryEventArgs);

        if (directoryEventArgs.Stop)
        {
          yield break;
        }

        if (!directoryEventArgs.Exclude && _filter(subDirectory))
        {
          var filteredDirectoryEventArgs = new DirectoryEventArgs { Path = subDirectory };
          OnFilteredDirectoryFound(directoryEventArgs);

          if (filteredDirectoryEventArgs.Stop)
          {
            yield break;
          }

          if (!filteredDirectoryEventArgs.Exclude)
          {
            yield return subDirectory;
          }
        }

        foreach (var item in SearchInDirectory(subDirectory))
        {
          yield return item;
        }
      }
    }

    private IEnumerable<string> SearchInFiles(string root)
    {
      foreach (var file in Directory.EnumerateFiles(root))
      {
        var fileEventArgs = new FileEventArgs { Path = file};
        OnFileFound(fileEventArgs);

        if (fileEventArgs.Stop)
        {
          yield break;
        }

        if (!fileEventArgs.Exclude && _filter(file))
        {
          var filteredFileEventArgs = new FileEventArgs { Path = file };
          OnFilteredFileFound(filteredFileEventArgs);

          if (filteredFileEventArgs.Stop)
          {
            yield break;
          }

          if (!filteredFileEventArgs.Exclude)
          {
            yield return file;
          }
        }
      }
    }

    public IEnumerator<string> GetEnumerator()
    {
      return SearchInFileSystem(dirRoot).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public event EventHandler<EventArgs> SearchStarted;
    public event EventHandler<EventArgs> SearchFinished;
    public event EventHandler<FileEventArgs> FileFound;
    public event EventHandler<DirectoryEventArgs> DirectoryFound;
    public event EventHandler<FileEventArgs> FilteredFileFound;
    public event EventHandler<DirectoryEventArgs> FilteredDirectoryFound;
    protected virtual void OnSearchStarted(EventArgs args)
    {
      SearchStarted?.Invoke(this, args);
    }

    protected virtual void OnSearchFinished(EventArgs args)
    {
      SearchFinished?.Invoke(this, args);
    }

    protected virtual void OnFileFound(FileEventArgs args)
    {
      FileFound?.Invoke(this, args);
    }

    protected virtual void OnDirectoryFound(DirectoryEventArgs args)
    {
      DirectoryFound?.Invoke(this, args);
    }

    protected virtual void OnFilteredFileFound(FileEventArgs args)
    {
      FilteredFileFound?.Invoke(this, args);
    }

    protected virtual void OnFilteredDirectoryFound(DirectoryEventArgs args)
    {
      FilteredDirectoryFound?.Invoke(this, args);
    }

 
  }
}
