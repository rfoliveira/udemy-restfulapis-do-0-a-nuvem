using System.Collections.Generic;
using System.Linq;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Models;

namespace RestWithASPNETUdemy.Data.Converter
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

        public List<Book> ParseList(List<BookVO> originList)
        {
            if (originList == null) return new List<Book>();

            return originList.Select(o => Parse(o)).ToList();
        }

        public List<BookVO> ParseList(List<Book> originList)
        {
            if (originList == null) return new List<BookVO>();

            return originList.Select(o => Parse(o)).ToList();
        }
    }
}