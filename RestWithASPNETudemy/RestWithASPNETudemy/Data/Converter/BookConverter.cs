using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETudemy.Data.Converter
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return new Book();

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null) return new BookVO();

            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                Title = origin.Title,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate
            };
        }

        public List<Book> ParseList(List<BookVO> origin)
        {
            if (origin == null) return new List<Book>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookVO> ParseList(List<Book> origin)
        {
            if (origin == null) return new List<BookVO>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
