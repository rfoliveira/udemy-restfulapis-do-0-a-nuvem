using System.Collections.Generic;

namespace RestWithASPNETUdemy.Hypermedia.Abstract
{
    public interface ISupportHypermedia
    {
         List<HypermediaLink> Links { get; set; }
    }
}