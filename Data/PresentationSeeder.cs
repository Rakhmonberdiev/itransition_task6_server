using itransition_task6_server.Models;

namespace itransition_task6_server.Data
{
    public static class PresentationSeeder
    {
        public static List<Presentation> GetSeedPresentations()
        {
            return new List<Presentation>
            {
                new Presentation
                {
                    Title = "Hello world!!!",
                    CreatorName = "system",
                    Slides = new List<Slide>
                    {
                        new Slide
                        {
                            Order = 0,
                            Elements = new List<SlideElement>
                            {
                                new TextBlock
                                {
                                    Text = "Welcome",
                                    X = 50,
                                    Y = 50,
                                    Width = 300,
                                    Height = 80,
                                    FontSize = 28,
                                    FontFamily = "Segoe UI",
                                    Color = "#1a1a1a"
                                },
                                new ImageBlock
                                {
                                    Url = "https://res.cloudinary.com/du9hzzzai/image/upload/v1754567442/itransition/dm652uy3fg98isg9t0ps.png",
                                    X = 100,
                                    Y = 200,
                                    Width = 400,
                                    Height = 400
                                }
                            }
                        }
                    }
                },
                new Presentation
                {
                    Title = "Hello world 2!!!",
                    CreatorName = "system",
                    Slides = new List<Slide>
                    {
                        new Slide
                        {
                            Order = 0,
                            Elements = new List<SlideElement>
                            {
                                new TextBlock
                                {
                                    Text = "Welcome",
                                    X = 50,
                                    Y = 50,
                                    Width = 300,
                                    Height = 80,
                                    FontSize = 28,
                                    FontFamily = "Segoe UI",
                                    Color = "#1a1a1a"
                                },
                                new ImageBlock
                                {
                                    Url = "https://res.cloudinary.com/du9hzzzai/image/upload/v1754567442/itransition/dm652uy3fg98isg9t0ps.png",
                                    X = 100,
                                    Y = 200,
                                    Width = 400,
                                    Height = 400
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
