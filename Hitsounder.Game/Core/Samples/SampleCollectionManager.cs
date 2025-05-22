using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hitsounder.Game.Database;
using Microsoft.EntityFrameworkCore;

namespace Hitsounder.Game.Core.Samples;

public class SampleCollectionManager
{
    private static readonly Dictionary<string, SampleSet> sample_set_keywords = new Dictionary<string, SampleSet>
    {
        { "normal-", SampleSet.Normal },
        { "drum-", SampleSet.Drum },
        { "soft-", SampleSet.Soft },
    };

    private static readonly Dictionary<string, SampleType> sample_type_keywords = new Dictionary<string, SampleType>
    {
        { "hitnormal", SampleType.Normal }, // hitnormal so we don't match normal-... filenames
        { "whistle", SampleType.Whistle },
        { "clap", SampleType.Clap },
        { "finish", SampleType.Finish },
    };

    private Dictionary<string, (SampleSet, SampleType)> sample_defaults_keywords = new Dictionary<string, (SampleSet, SampleType)>
    {
        { "hihat", (SampleSet.Drum, SampleType.Whistle) },
        { "hi-hat", (SampleSet.Drum, SampleType.Whistle) },
        { "tom", (SampleSet.Drum, SampleType.Finish) },
        { "crash", (SampleSet.Soft, SampleType.Finish) },
        { "cymbal", (SampleSet.Soft, SampleType.Finish) },
        { "snare", (SampleSet.Normal, SampleType.Normal) },
        { "kick", (SampleSet.Drum, SampleType.Normal) },
        { "bass", (SampleSet.Drum, SampleType.Normal) },
    };

    private readonly DbAccess db;

    public SampleCollectionManager(DbAccess db)
    {
        this.db = db;
    }

    public List<SampleCollectionInfo> GetAll()
    {
        return db.SampleCollections
                 .Include(c => c.Samples)
                 .ToList();
    }

    public SampleCollectionInfo ImportFromDirectory(string directory, bool global = true)
    {
        if (!Directory.Exists(directory))
            throw new DirectoryNotFoundException(directory);

        var files = Directory
                    .GetFiles(directory, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wav") || s.EndsWith(".ogg"));

        return db.Write(db =>
        {
            var samples = new List<HitSoundSampleInfo>();

            foreach (var filePath in files)
            {
                var (sampleSet, sampleType) = detectDefaultsFromPath(filePath);

                var sample = new HitSoundSampleInfo
                {
                    Name = Path.GetRelativePath(directory, filePath).TrimStart('.', '/'),
                    Path = filePath,
                    DefaultSampleSet = sampleSet,
                    DefaultSampleType = sampleType,
                };

                samples.Add(sample);

                db.HitSoundSamples.Add(sample);
            }

            var collection = new SampleCollectionInfo
            {
                Name = Path.GetFileName(directory),
                Global = global,
                Samples = samples,
            };

            db.SampleCollections.Add(collection);

            return collection;
        });
    }

    private (SampleSet, SampleType) detectDefaultsFromPath(string path)
    {
        var filename = Path.GetFileNameWithoutExtension(path);
        var directoryName = Path.GetDirectoryName(path);

        var (sampleSet, sampleType) = detectFromString(sample_defaults_keywords, filename, directoryName, (SampleSet.Auto, SampleType.None));

        if (sampleSet == SampleSet.Auto)
            sampleSet = detectFromString(sample_set_keywords, filename, directoryName, SampleSet.Auto);

        if (sampleType == SampleType.None)
            sampleType = detectFromString(sample_type_keywords, filename, directoryName, SampleType.None);

        return (sampleSet, sampleType);

        T detectFromString<T>(Dictionary<string, T> keywords, string searchString, string? fallbackSearchString, T defaultValue)
        {
            foreach (var (key, value) in keywords)
            {
                if (searchString.ToLowerInvariant().Contains(key))
                    return value;
            }

            if (fallbackSearchString != null)
            {
                foreach (var (key, value) in keywords)
                {
                    if (fallbackSearchString.ToLowerInvariant().Contains(key))
                        return value;
                }
            }

            return defaultValue;
        }
    }
}
