using System;
using System.IO;
using System.Drawing;
using System.Collections;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace CallOfCSharp
{
    public class MapGetter
    {
        private short sizeX = 0;
        private short sizeZ = 0;

        public void ReadFromFile(string fileName, Device device, CallOfCS cocs)
        {
            ArrayList addLater = new ArrayList();
            try
            {                
                Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + @"\data\");
                StreamReader tr = new StreamReader(fileName, System.Text.Encoding.ASCII);
                string ln;
                string[] tokens = tr.ReadLine().Split('x');
                if (tokens.Length != 2)
                {
                    System.Console.WriteLine("Map file is corrupted. Size is not defined.");
                    System.Environment.Exit(0);
                }

                try
                {
                    sizeX = Convert.ToInt16(tokens[0]);
                    sizeZ = Convert.ToInt16(tokens[1]);
                    if (sizeX > 100 || sizeZ > 100)
                    {
                        System.Console.WriteLine("Map file is corrupted. Size of map is not defined properly.");
                        System.Environment.Exit(0);
                    }
                }
                catch (FormatException e)
                {
                    System.Console.WriteLine("Map file is corrupted. Size is not defined properly."+e.ToString());
                    System.Environment.Exit(0);
                }                
                cocs.universe.Add(new Floor(device, sizeX, sizeZ, cocs));

                cocs.mapSizeX = sizeX;
                cocs.mapSizeZ = sizeZ;
                cocs.mapRealSizeX = cocs.mapSizeX * cocs.size_of_one_rectangle;
                cocs.mapRealSizeZ = cocs.mapSizeZ * cocs.size_of_one_rectangle;

                cocs.aArea = new bool[sizeX, sizeZ];
                for (short k = 0; k < sizeX; k++)
                {
                    for (short l = 0; l < sizeZ; l++)
                    {
                        cocs.aArea[k, l] = true;
                    }
                }

                short j = sizeZ;
                while (((ln = tr.ReadLine()) != null) && j > 0)
                {
                    j--;
                    for (short i = 0; i < Math.Min(sizeX, ln.Length); i++)
                    {
                        switch (ln[i])
                        {
                            case 'A':
                            case 'B':
                            case 'C':
                            case 'D':                                
                            case 'E':
                            case 'F':
                            case 'G':
                                Object someobject = new Wall(device, cocs, i, j);
                                cocs.universe.Add(someobject);
                                cocs.aArea[i, j] = false;
                                cocs.actionObjects.Add(someobject);
                                break;
                            case '*':
                            case 'a':
                            case 'b':
                            case 'c':
                            case 'd':                                
                            case 'e':
                            case 'f':
                            case 'g':
                            case 'h':
                            case 'i':
                            case 'j':
                            case 'k':
                            case 'l':
                            case 'm':
                            case 'n':
                            case 'H':
                            case 'I':
                            case 'J':
                            case 'K':
                            case 'L':
                            case 'M':
                            case 'N':                                
                                cocs.universe.Add(new Wall(device, cocs, i, j));
                                cocs.aArea[i, j] = false;
                                break;
                            case 'O':
                            case 'P':
                            case 'Q':
                            case 'R':
                            case 'S':
                                cocs.myPosX = cocs.size_of_one_rectangle * (i + 0.5f);
                                cocs.myPosZ = cocs.size_of_one_rectangle * (j + 0.5f);
                                break;
                            case 'T':
                                someobject = new Target(device, cocs, i, j);
                                //addLater.Add(someobject);
                                cocs.universe.Add(someobject);
                                cocs.actionObjects.Add(someobject);
                                cocs.numberOfTargets++;
                                break;
                            case 'U':
                            case 'V':
                            case 'W':
                            case 'X':
                            case 'Y':
                            case 'Z':
                                someobject = new Cube(device, cocs, i, j);
                                addLater.Add(someobject);
                                cocs.actionObjects.Add(someobject);
                                cocs.numberOfItems++;
                                break;
                            case '#':
                                addLater.Add(new Cube(device, cocs, i, j));
                                cocs.aArea[i, j] = false;
                                break;
                        }

                    }                    
                }
                foreach (CommonClass ikx in addLater)                
                    cocs.universe.Add(ikx);                
                tr.Close();
            }
            catch (DirectoryNotFoundException e)
            {
                System.Console.WriteLine("Directory not found"+e.ToString());
                System.Environment.Exit(0);
            }
            catch (FileNotFoundException e)
            {
                System.Console.WriteLine("File : " + fileName + " not found!"+e.ToString());
                System.Environment.Exit(0);
            }           
        }
    }
}