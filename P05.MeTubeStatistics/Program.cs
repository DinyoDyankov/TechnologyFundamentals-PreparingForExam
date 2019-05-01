namespace P05.MeTubeStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public class Video
        {
            public Video(string name, int views, int likes)
            {
                this.Name = name;
                this.Views = views;
                this.Likes = likes;
            }

            public string Name { get; set; }
            public int Views { get; set; }
            public int Likes { get; set; }
        }

        public static void Main()
        {
            string input = string.Empty;

            List<Video> allVideos = new List<Video>();

            while ((input = Console.ReadLine()) != "stats time")
            {

                if (!input.StartsWith("like:") && !input.StartsWith("dislike:"))
                {
                    string[] currentVideo = input.Split('-');

                    string videoName = currentVideo[0];
                    int views = int.Parse(currentVideo[1]);
                    int likes = 0;

                    if (allVideos.Any(v => v.Name.Contains(videoName)))
                    {
                        int index = allVideos.FindIndex(v => v.Name == videoName);

                        allVideos[index].Views += views;
                    }
                    else
                    {
                        Video videoToAdd =  new Video(videoName, views, likes);

                        allVideos.Add(videoToAdd);
                    }
                }
                else
                {
                    string[] command = input.Split(':');

                    if (allVideos.Any(v => v.Name.Contains(command[1])))
                    {
                        int index = allVideos.FindIndex(v => v.Name == command[1]);

                        if (command[0] == "like")
                        {
                            allVideos[index].Likes += 1;
                        }
                        else
                        {
                            allVideos[index].Likes -= 1;
                        }
                    }
                }
            }

            string outputCommand = Console.ReadLine();

            if (outputCommand == "by views")
            {
                foreach (var video in allVideos.OrderByDescending(v => v.Views))
                {
                    Console.WriteLine($"{video.Name} - {video.Views} views - {video.Likes} likes");
                }
            }
            else
            {
                foreach (var video in allVideos.OrderByDescending(v => v.Likes))
                {
                    Console.WriteLine($"{video.Name} - {video.Views} views - {video.Likes} likes");
                }
            }
        }
    }
}
