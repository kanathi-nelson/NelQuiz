using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Viewmodels
{
    public class FileDetails
    {
        public string FName { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; }
            = new List<FileDetails>();
    }
    public class MyFileModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string Fname { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
