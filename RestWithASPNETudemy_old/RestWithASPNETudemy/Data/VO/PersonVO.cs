namespace RestWithASPNETudemy.Data.VO
{
    public class PersonVO//: ISupportsHyperMedia
    {
        public long? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Genre { get; set; }

        // HATEOAS (não funcionou)
        //public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
