using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class WriteFile{

    private string path;

    public WriteFile(string path) {
        this.path = path;
        if(File.Exists(path))
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(path);
            f.Write("");
            f.Close();
        }
    }

    public void writeLine(string line){
        if(File.Exists(path))
        {
            StreamWriter w = File.AppendText(path);
            w.WriteLine(line);
            w.Close();
        }
        else
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(path);
            f.WriteLine(line);
            f.Close();
        }
    }

    public void writeAllLines(List<string> lines) {
        if (File.Exists(path))
        {
            StreamWriter w = File.AppendText(path);
            foreach(string l in lines)
            {
                w.WriteLine(l);
            }
            w.Close();
        }
        else
        {
            System.IO.StreamWriter f = new System.IO.StreamWriter(path);
            foreach(string l in lines)
            {
                f.WriteLine(l);
            }
            f.Close();
        }
    }
}
