namespace Week7_Lab.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Week7_Lab.Models.PinterestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Week7_Lab.Models.PinterestDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<PinterestUser>(new UserStore<PinterestUser>(context));

            if (userManager.FindByName("ahudson") == null)
            {
                var aaron = new PinterestUser() { FirstName = "Aaron", LastName = "Hudson", Email = "aaron@theironyard.com", UserName = "aaron@theironyard.com" };
                userManager.Create(aaron, "Password123!");
            }

            context.SaveChanges();

            var user = context.Users.FirstOrDefault(x => x.UserName == "aaron@theironyard.com");
            context.Pins.AddOrUpdate(
                 new Models.Pin { Image = GetImageByteArray("http://scrapetv.com/News/News%20Pages/Entertainment/images-3/star-wars-poster.jpg"), ImageLink = "http://www.imdb.com/title/tt0076759", Note = "They teach a slave boy to become a Jedi, who later becomes mutilated and becomes dedicated to evil. They find a new boy who is pure of heart and teach him to fight Darth Vader.", WhoPinned = user, PinId = 1 },
                 new Models.Pin { Image = GetImageByteArray("http://fc06.deviantart.net/fs51/f/2009/258/2/f/2fca1bc9ddcc147ef674d2eebb14ee3a.jpg"), ImageLink = "http://www.imdb.com/title/tt0133093", Note = "A black man comes to a white guy named Neo and asks him to choose between colours, which unbeknownst to him represented life choices. When he chooses the pill that 'goes down the rabbit hole', he gets thrust into a digital world in which robots have taken over the world secretly and they have super powers.", WhoPinned = user },
                 new Models.Pin { Image = GetImageByteArray("http://www.bollygraph.com/wp-content/uploads/2013/01/Godfather-1972.jpg"), ImageLink = "http://www.imdb.com/title/tt0068646/", Note = "It’s about a mob family, and I’m not sure about the rest. There is a quote at the end where the godfather says something, but I don’t remember what it is.", WhoPinned = user },
                 new Models.Pin { Image = GetImageByteArray("http://img4.wikia.nocookie.net/__cb20090618213543/indianajones/images/0/04/Raidersteaser.jpg"), ImageLink = "http://www.imdb.com/title/tt0082971/", Note = "blah", WhoPinned = user, PinId = 4 },
                 new Models.Pin { Image = GetImageByteArray("https://pabblogger.files.wordpress.com/2011/03/dark_knight.jpg"), ImageLink = "http://www.imdb.com/title/tt0468569", Note = "blah", WhoPinned = user, PinId = 5 },
                 new Models.Pin { Image = GetImageByteArray("http://4.bp.blogspot.com/-sY6VnEIBCNU/UNSMSN7zo9I/AAAAAAAAAZ8/0OORh5M-V70/s1600/The+Terminator+Poster.jpg"), ImageLink = "http://www.imdb.com/title/tt0088247", Note = "blah", WhoPinned = user, PinId = 6 },
                 new Models.Pin { Image = GetImageByteArray("https://fanart.tv/fanart/movies/278/movieposter/the-shawshank-redemption-5223c8231afe1.jpg"), ImageLink = "http://www.imdb.com/title/tt0111161", Note = "blah", WhoPinned = user, PinId = 7 }
                );
        }

        public static byte[] GetImageByteArray(string ImageUrl)
        {
            var client = new WebClient();
            var imageArray = client.DownloadData(ImageUrl);
            var bytearraytoputindb = Models.Pin.ScaleImage(imageArray, 100, 100);
            return bytearraytoputindb;
        }
    }
}
