using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileVisitor
{
  public class FileEventArgs : EventArgs
  {
    public string Path { get; set; }
    public bool Stop { get; set; }
    public bool Exclude { get; set; }
  }
}
