using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PicasaWPF
{
    public class AlbumObject
    {
        public String Name { get; set; }
        public byte[] Image { get; set; }
        public AlbumObject(String Name, byte[] Image)
        {
            this.Name = Name;
            this.Image = Image;
        }
    }
    public class AlbumCollection : ObservableCollection<AlbumObject> { }
}
