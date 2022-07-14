using Google.Apis.YouTube.v3.Data;

namespace CoonBot.Connections {
	public record YouTubeVideoData {
		public string Url { get; protected set; }
		public string Title { get; protected set; }
		public string Description { get; protected set; }
		public string Thumbnail { get; protected set; }
		public string Channel { get; protected set; }

		public SearchResult RawData { get; protected set; }
		
		public YouTubeVideoData(SearchResult result) {
			RawData = result;
			
			Url = $"https://www.youtube.com/watch?v={result.Id.VideoId}";
			Title = result.Snippet.Title ?? "";
			Description = result.Snippet.Description ?? "";
			Thumbnail = result.Snippet.Thumbnails.High.Url;
			Channel = result.Snippet.ChannelTitle ?? "";
		}
	}
}
