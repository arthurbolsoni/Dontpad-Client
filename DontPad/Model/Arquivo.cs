using DontPad.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontPad.Model
{
    public class Arquivo : ModelBase
    {
        private string _body;
        public string body
        {
            get => _body;
            set => SetField(ref _body, value, nameof(body));
        }

        private bool _changed = false;
        public bool changed
        {
            get => _changed;
            set => SetField(ref _changed, value, nameof(changed));
        }

        private long _lastUpdate = 0;
        public long lastUpdate
        {
            get => _lastUpdate;
            set => SetField(ref _lastUpdate, value, nameof(lastUpdate));
        }

        private List<String> _menu;
        public List<String> menu
        {
            get => _menu;
            set => SetField(ref _menu, value, nameof(menu));
        }
    }
}
