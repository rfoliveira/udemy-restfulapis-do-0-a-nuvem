using System.Text;

namespace RestWithASPNETUdemy.Hypermedia
{
    public class HypermediaLink
    {
        private string _href;

        public string Rel { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        
        /*
            Para deixar a URL mais legível, 
            é acrescentado tratamento na conversão de "/" para "%2f".
            Esse tratamento precisa ser feito com lock porque
            ele é executado em paralelo.
        */            
        public string Href 
        {
            get
            {
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(_href);
                    return sb.Replace("%2F", "/").ToString();
                }
            } 
            set => _href = value; 
        }
    }
}