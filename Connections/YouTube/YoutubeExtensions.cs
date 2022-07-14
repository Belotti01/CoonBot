using Google.Apis.YouTube.v3;

namespace CoonBot.Connections {
	public static class YoutubeExtensions {

		public static async Task<YouTubeVideoData[]> SearchVideos(this YouTubeService service, string query) {
            var searchListRequest = service.Search.List("snippet");
            searchListRequest.Q = query;
            searchListRequest.Type = "video";
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            var searchListResponse = await searchListRequest.ExecuteAsync();
            return searchListResponse.Items
                .Select(x => new YouTubeVideoData(x))
                .ToArray();
        }
	}
}
