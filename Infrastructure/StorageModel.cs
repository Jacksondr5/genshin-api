using System.Collections.Generic;
using Core;

namespace Infrastructure
{
    internal class StorageModel
    {
        public List<Artifact> Artifacts { get; set; } = new();
        public List<Character> Characters { get; set; } = new();
    }
}