namespace Week7_Lab.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Net;

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

            var img1 = GetImageByteArray("http://scrapetv.com/News/News%20Pages/Entertainment/images-3/star-wars-poster.jpg");
            var img2 = GetImageByteArray("http://fc06.deviantart.net/fs51/f/2009/258/2/f/2fca1bc9ddcc147ef674d2eebb14ee3a.jpg");
            var img3 = GetImageByteArray("http://www.bollygraph.com/wp-content/uploads/2013/01/Godfather-1972.jpg");
            var img4 = GetImageByteArray("http://img4.wikia.nocookie.net/__cb20090618213543/indianajones/images/0/04/Raidersteaser.jpg");
            var img5 = GetImageByteArray("https://pabblogger.files.wordpress.com/2011/03/dark_knight.jpg");
            var img6 = GetImageByteArray("http://4.bp.blogspot.com/-sY6VnEIBCNU/UNSMSN7zo9I/AAAAAAAAAZ8/0OORh5M-V70/s1600/The+Terminator+Poster.jpg");
            var img7 = GetImageByteArray("https://fanart.tv/fanart/movies/278/movieposter/the-shawshank-redemption-5223c8231afe1.jpg");
            var user = context.Users.FirstOrDefault(x => x.UserName == "aaron@theironyard.com");
            context.Pins.AddOrUpdate(p => p.ImageLink,
                 new Models.Pin { Image = img1, ImageLink = "http://www.imdb.com/title/tt0076759", Note = "They teach a slave boy to become a Jedi, who later becomes mutilated and becomes dedicated to evil. They find a new boy who is pure of heart and teach him to fight Darth Vader.", WhoPinned = user },
                 new Models.Pin { Image = img2, ImageLink = "http://www.imdb.com/title/tt0133093", Note = "A black man comes to a white guy named Neo and asks him to choose between colours, which unbeknownst to him represented life choices. When he chooses the pill that 'goes down the rabbit hole', he gets thrust into a digital world in which robots have taken over the world secretly and they have super powers.", WhoPinned = user },
                 new Models.Pin { Image = img3, ImageLink = "http://www.imdb.com/title/tt0068646/", Note = "It’s about a mob family, and I’m not sure about the rest. There is a quote at the end where the godfather says something, but I don’t remember what it is.", WhoPinned = user },
                 new Models.Pin { Image = img4, ImageLink = "http://www.imdb.com/title/tt0082971/", Note = "A guy who runs into tombs and pyramids and tries to collect artifacts, but always runs into boobie traps. Also, there are cursed people and things that he has to deal with as well.", WhoPinned = user },
                 new Models.Pin { Image = img5, ImageLink = "http://www.imdb.com/title/tt0468569", Note = "A group of people control the financial and political systems of as much as they can get their hands on. Batmans parents donate to the poor and create programs to prevent that from happening. Thus, they try another method to destroy it by releasing a toxin that makes people go criminally insane. However, batman manages to foil their plans.", WhoPinned = user },
                 new Models.Pin { Image = img6, ImageLink = "http://www.imdb.com/title/tt0088247", Note = "A movie about a robot who travels back in time to save, what I think I remember being as his inventor, from evil robots who try to change the future by killing him.", WhoPinned = user },
                 new Models.Pin { Image = img7, ImageLink = "http://www.imdb.com/title/tt0111161", Note = "A scary movie about people who run into some crazy person that wants to kill them.", WhoPinned = user }
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
