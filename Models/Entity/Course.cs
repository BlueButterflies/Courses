using Courses.Models.Services.Application;
using System;
using System.Collections.Generic;

#nullable disable

namespace Courses.Models.Entity
{
    public partial class Course
    {
        public Course(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("The course must have a title");
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("The course must have an author");
            }

            this.Title = title;
            this.Author = author;
        }


        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Descriptions { get; private set; }

        public string ImagePath { get; private set; }

        public string Author { get; private set; }

        public string Email { get; private set; }

        public float Rating { get; private set; }

        public Money FullPrice { get; private set; }

        public Money DiscountPrice { get; private set; }

        public ICollection<Lesson> Lessons { get; private set; }

        public void ChangeTitle (string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("The course must have a title");
            }

            this.Title = newTitle;
        }

        public void ChangePrices(Money newFullPrice, Money newDiscountPrice)
        {
            if (newFullPrice == null || newDiscountPrice == null)
            {
                throw new ArgumentException("Prices can't be null");
            }

            if (newFullPrice.Currency != newDiscountPrice.Currency)
            {
                throw new ArgumentException("Currencies don't mutch");
            }

            if (newFullPrice.Amount < newDiscountPrice.Amount)
            {
                throw new ArgumentException("Full price can't be less than current discount price");
            }

            this.FullPrice = newFullPrice;
            this.DiscountPrice = newDiscountPrice;
        }
    }
}
