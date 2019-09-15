using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LoadSongFromFile : MonoBehaviour
{
    static char COMMENT = '#';
    static char NOTE = '-';
    static char NOTE_RELATIVE = '+';
    static char VALUE_DELIM = '=';
    static char NOTE_DELIM = ',';

    static Note ParseNote(string line)
    {
        //Format
        //-[bar],[beat],[offset],[lane], [optional arguments of the form x=y]
        //-0,1,0,0,noteType=up,soundType=normal
        //-0,1,0,0,nt=up,st=normal
        //-0,1,0,0

        string[] parts = line.Substring(1).Split(NOTE_DELIM);
        Assert.IsTrue(parts.Length >= 4);
        Note toAdd = new Note();
        toAdd.timing = new Timestamp(int.Parse(parts[0]), int.Parse(parts[1]), float.Parse(parts[2]));
        toAdd.lane = int.Parse(parts[3]);
        //Defaults
        toAdd.type = NoteType.Hit;
        toAdd.soundType = SoundType.Normal;

        for(int i = 4; i < parts.Length; ++i)
        {
            if(parts[i].Contains("="))
            {
                string[] subparts = parts[i].Split('=');
                string name = subparts[0].ToLower().Trim();
                switch(name)
                {
                    case "notetype":
                    case "type":
                    case "nt":
                    case "n":
                    case "t":
                        switch(subparts[1].ToLower().Trim())
                        {
                            case "hit":
                            case "h":
                                toAdd.type = NoteType.Hit;
                                break;
                            case "uphit":
                            case "up":
                            case "u":
                            case "uh":
                                toAdd.type = NoteType.UpHit;
                                break;
                            case "downhit":
                            case "down":
                            case "d":
                            case "dh":
                                toAdd.type = NoteType.DownHit;
                                break;
                        }
                        break;
                    case "soundtype":
                    case "st":
                    case "s":
                        switch(subparts[1].ToLower().Trim())
                        {
                            case "normal":
                            case "n":
                                toAdd.soundType = SoundType.Normal;
                                break;
                        }
                        break;
                }
            }
        }
        Debug.Log(toAdd.type);
        return toAdd;
    }

    public static LoadedSong LoadSong(TextAsset file, AudioClip audio)
    {
        LoadedSong song = (LoadedSong)ScriptableObject.CreateInstance("LoadedSong");
        song.notes = new List<Note>();
        song.song = audio;
        StringReader reader = new StringReader(file.text);
        Timestamp lastTime = new Timestamp(0,0,0);
        while(reader.Peek() > -1)
        {
            string line = reader.ReadLine();

            if(line.Length < 1)
                continue;
            if(line[0] == COMMENT)
                continue;
            
            if(line[0] == NOTE)
            {
                Note toAdd = ParseNote(line);
                song.notes.Add(toAdd);
                lastTime = toAdd.timing;
            }
            else if(line[0] == NOTE_RELATIVE)
            {
                Note toAdd = ParseNote(line);
                toAdd.timing.addTicks(song.beatsPerMeasure, lastTime.getTicks(song.beatsPerMeasure));
                song.notes.Add(toAdd);
                lastTime = toAdd.timing;
            }
            else if(line.Contains(VALUE_DELIM.ToString()))
            {
                string[] parts = line.Split(VALUE_DELIM);
                if(parts[0].ToLower().Trim().Equals("bpm"))
                {
                    Assert.IsTrue(float.TryParse(parts[1], out song.bpm));
                }
                else if(parts[0].ToLower().Trim().Equals("offset"))
                {
                    Assert.IsTrue(float.TryParse(parts[1], out song.offset));
                }
                else if(parts[0].ToLower().Trim().Equals("beatspermeasure"))
                {
                    Assert.IsTrue(int.TryParse(parts[1], out song.beatsPerMeasure));
                }
                else if(parts[0].ToLower().Trim().Equals("name"))
                {
                    song.name = parts[1];
                }
            }
        }
        return song;
    }

    public TextAsset file;

    new public AudioClip audio;
}
