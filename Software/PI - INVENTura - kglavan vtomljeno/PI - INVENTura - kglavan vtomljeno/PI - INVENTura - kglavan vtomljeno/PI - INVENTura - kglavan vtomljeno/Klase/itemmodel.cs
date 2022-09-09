using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PI___INVENTura___kglavan_vtomljeno
{
    public class itemmodel : INotifyPropertyChanged
    {
        public string item, size;
        public decimal qty, price, total;
        
        public string Item
        {
            get => item;
            set { item = value; OnPropertyChanged(); }
        }

        public string Size
        {
            get => size;
            set { size = value; OnPropertyChanged(); }
        }

        public decimal Quantity
        {
            get => qty;
            set { qty = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(); }
        }

        public decimal Total
        {
            get => total;
            
            set { total = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This is to notify the grid if any of the properties are updated
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
