using System.Data.SQLite;

namespace MikanParserDotNetByBanned.models.sql
{
    internal class BangumiInfoSqlManager
    {
        private readonly string _connectionString;

        // Create tables if they don't exist
        private const string CreateBangumiApiSubjectJsonTable = """
                                                                                CREATE TABLE IF NOT EXISTS BangumiApiSubjectJson (
                                                                                    Id INTEGER PRIMARY KEY,
                                                                                    Date TEXT,
                                                                                    Platform TEXT,
                                                                                    Summary TEXT,
                                                                                    Name TEXT,
                                                                                    NameCn TEXT,
                                                                                    TotalEpisodes INTEGER,
                                                                                    Eps INTEGER,
                                                                                    Type INTEGER,
                                                                                    RatingRank INTEGER,
                                                                                    RatingTotal INTEGER,
                                                                                    RatingScore REAL,
                                                                                    ImagesSmall TEXT,
                                                                                    ImagesGrid TEXT,
                                                                                    ImagesLarge TEXT,
                                                                                    ImagesMedium TEXT,
                                                                                    ImagesCommon TEXT
                                                                                )
                                                                """;

        private const string CreateTagTable = """
                                                              CREATE TABLE IF NOT EXISTS Tag (
                                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                  BangumiApiSubjectJsonId INTEGER,
                                                                  Name TEXT,
                                                                  Count INTEGER,
                                                                  FOREIGN KEY (BangumiApiSubjectJsonId) REFERENCES BangumiApiSubjectJson(Id)
                                                              )
                                              """;

        private const string CreateInfoBoxTable = """
                                                                  CREATE TABLE IF NOT EXISTS InfoBox (
                                                                      Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                      BangumiApiSubjectJsonId INTEGER,
                                                                      Key TEXT,
                                                                      Value TEXT,
                                                                      FOREIGN KEY (BangumiApiSubjectJsonId) REFERENCES BangumiApiSubjectJson(Id)
                                                                  )
                                                  """;

        private const string CreateRatingCountTable = """
                                                                      CREATE TABLE IF NOT EXISTS RatingCount (
                                                                          Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                          BangumiApiSubjectJsonId INTEGER,
                                                                          CountKey INTEGER,
                                                                          CountValue INTEGER,
                                                                          FOREIGN KEY (BangumiApiSubjectJsonId) REFERENCES BangumiApiSubjectJson(Id)
                                                                      )
                                                      """;

        public BangumiInfoSqlManager(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();


            ExecuteNonQuery(connection, CreateBangumiApiSubjectJsonTable);
            ExecuteNonQuery(connection, CreateTagTable);
            ExecuteNonQuery(connection, CreateInfoBoxTable);
            ExecuteNonQuery(connection, CreateRatingCountTable);
            connection.Close();
        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string commandText)
        {
            using var command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
        }

        public void InsertBangumiApiSubjectJson(BangumiApiSubjectJson subject)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            const string insertSubject = """
                                         
                                                         INSERT INTO BangumiApiSubjectJson
                                                         (Id, Date, Platform, Summary, Name, NameCn, TotalEpisodes, Eps, Type, RatingRank, RatingTotal, RatingScore, ImagesSmall, ImagesGrid, ImagesLarge, ImagesMedium, ImagesCommon)
                                                         VALUES (@Id, @Date, @Platform, @Summary, @Name, @NameCn, @TotalEpisodes, @Eps, @Type, @RatingRank, @RatingTotal, @RatingScore, @ImagesSmall, @ImagesGrid, @ImagesLarge, @ImagesMedium, @ImagesCommon)
                                         """;

            using (var command = new SQLiteCommand(insertSubject, connection))
            {
                command.Parameters.AddWithValue("@Id", subject.Id);
                command.Parameters.AddWithValue("@Date", subject.Date);
                command.Parameters.AddWithValue("@Platform", subject.Platform);
                command.Parameters.AddWithValue("@Summary", subject.Summary);
                command.Parameters.AddWithValue("@Name", subject.Name);
                command.Parameters.AddWithValue("@NameCn", subject.NameCn);
                command.Parameters.AddWithValue("@TotalEpisodes", subject.TotalEpisodes);
                command.Parameters.AddWithValue("@Eps", subject.Eps);
                command.Parameters.AddWithValue("@Type", subject.Type);
                command.Parameters.AddWithValue("@RatingRank", subject.Rating?.Rank   ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RatingTotal", subject.Rating?.Total ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RatingScore", subject.Rating?.Score ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ImagesSmall", subject.Images?.Small);
                command.Parameters.AddWithValue("@ImagesGrid", subject.Images?.Grid);
                command.Parameters.AddWithValue("@ImagesLarge", subject.Images?.Large);
                command.Parameters.AddWithValue("@ImagesMedium", subject.Images?.Medium);
                command.Parameters.AddWithValue("@ImagesCommon", subject.Images?.Common);
                command.ExecuteNonQuery();
            }

            InsertTags(connection, subject.Id, subject.Tags!);
            InsertInfoBoxes(connection, subject.Id, subject.InfoBox!);
            InsertRatingCounts(connection, subject.Id, subject.Rating?.Count!);
            connection.Close();
        }

        private static void InsertTags(SQLiteConnection connection, int subjectId, List<Tag> ? tags)
        {
            if (tags == null) return;

            const string insertTag =
                "INSERT INTO Tag (BangumiApiSubjectJsonId, Name, Count) VALUES (@BangumiApiSubjectJsonId, @Name, @Count)";
            foreach (var tag in tags)
            {
                using var command = new SQLiteCommand(insertTag, connection);
                command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", subjectId);
                command.Parameters.AddWithValue("@Name", tag.Name);
                command.Parameters.AddWithValue("@Count", tag.Count);
                command.ExecuteNonQuery();
            }
        }

        private static void InsertInfoBoxes(SQLiteConnection connection, int subjectId, List<InfoBox> ? infoBoxes)
        {
            if (infoBoxes == null) return;

            const string insertInfoBox =
                "INSERT INTO InfoBox (BangumiApiSubjectJsonId, Key, Value) VALUES (@BangumiApiSubjectJsonId, @Key, @Value)";
            foreach (var infoBox in infoBoxes)
            {
                using var command = new SQLiteCommand(insertInfoBox, connection);
                command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", subjectId);
                command.Parameters.AddWithValue("@Key", infoBox.Key);
                command.Parameters.AddWithValue("@Value", infoBox.Value);
                command.ExecuteNonQuery();
            }
        }

        private static void InsertRatingCounts(SQLiteConnection       connection, int subjectId,
                                               Dictionary<int, int> ? ratingCounts)
        {
            if (ratingCounts == null) return;

            const string insertRatingCount =
                "INSERT INTO RatingCount (BangumiApiSubjectJsonId, CountKey, CountValue) VALUES (@BangumiApiSubjectJsonId, @CountKey, @CountValue)";
            foreach (var count in ratingCounts)
            {
                using var command = new SQLiteCommand(insertRatingCount, connection);
                command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", subjectId);
                command.Parameters.AddWithValue("@CountKey", count.Key);
                command.Parameters.AddWithValue("@CountValue", count.Value);
                command.ExecuteNonQuery();
            }
        }

        public BangumiApiSubjectJson ? GetBangumiApiSubjectJson(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            const string selectSubject = "SELECT * FROM BangumiApiSubjectJson WHERE Id = @Id";
            using (var command = new SQLiteCommand(selectSubject, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var subject = new BangumiApiSubjectJson
                        {
                            Id            = reader.GetInt32(0),
                            Date          = reader.GetString(1),
                            Platform      = reader.GetString(2),
                            Summary       = reader.GetString(3),
                            Name          = reader.GetString(4),
                            NameCn        = reader.GetString(5),
                            TotalEpisodes = reader.GetInt32(6),
                            Eps           = reader.GetInt32(7),
                            Type          = reader.GetInt32(8),
                            Rating = new Rating
                            {
                                Rank  = reader.GetInt32(9),
                                Total = reader.GetInt32(10),
                                Score = reader.GetDouble(11)
                            },
                            Images = new Images
                            {
                                Small  = reader.GetString(12),
                                Grid   = reader.GetString(13),
                                Large  = reader.GetString(14),
                                Medium = reader.GetString(15),
                                Common = reader.GetString(16)
                            },
                            Tags    = GetTags(connection, id),
                            InfoBox = GetInfoBoxes(connection, id)
                        };

                        connection.Close();
                        return subject;
                    }
                }
            }

            connection.Close();
            return null;
        }

        private static List<Tag> GetTags(SQLiteConnection connection, int subjectId)
        {
            var tags = new List<Tag>();

            const string selectTags =
                "SELECT Name, Count FROM Tag WHERE BangumiApiSubjectJsonId = @BangumiApiSubjectJsonId";
            using var command = new SQLiteCommand(selectTags, connection);
            command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", subjectId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tags.Add(new Tag
                {
                    Name  = reader.GetString(0),
                    Count = reader.GetInt32(1)
                });
            }

            return tags;
        }

        private static List<InfoBox> GetInfoBoxes(SQLiteConnection connection, int subjectId)
        {
            var infoBoxes = new List<InfoBox>();

            const string selectInfoBoxes =
                "SELECT Key, Value FROM InfoBox WHERE BangumiApiSubjectJsonId = @BangumiApiSubjectJsonId";
            using var command = new SQLiteCommand(selectInfoBoxes, connection);
            command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", subjectId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                infoBoxes.Add(new InfoBox
                {
                    Key   = reader.GetString(0),
                    Value = reader.GetValue(1)
                });
            }

            return infoBoxes;
        }

        public void UpdateBangumiApiSubjectJson(BangumiApiSubjectJson subject)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            const string updateSubject = """
                                         
                                                         UPDATE BangumiApiSubjectJson SET
                                                         Date = @Date,
                                                         Platform = @Platform,
                                                         Summary = @Summary,
                                                         Name = @Name,
                                                         NameCn = @NameCn,
                                                         TotalEpisodes = @TotalEpisodes,
                                                         Eps = @Eps,
                                                         Type = @Type,
                                                         RatingRank = @RatingRank,
                                                         RatingTotal = @RatingTotal,
                                                         RatingScore = @RatingScore,
                                                         ImagesSmall = @ImagesSmall,
                                                         ImagesGrid = @ImagesGrid,
                                                         ImagesLarge = @ImagesLarge,
                                                         ImagesMedium = @ImagesMedium,
                                                         ImagesCommon = @ImagesCommon
                                                         WHERE Id = @Id
                                         """;

            using (var command = new SQLiteCommand(updateSubject, connection))
            {
                command.Parameters.AddWithValue("@Id", subject.Id);
                command.Parameters.AddWithValue("@Date", subject.Date);
                command.Parameters.AddWithValue("@Platform", subject.Platform);
                command.Parameters.AddWithValue("@Summary", subject.Summary);
                command.Parameters.AddWithValue("@Name", subject.Name);
                command.Parameters.AddWithValue("@NameCn", subject.NameCn);
                command.Parameters.AddWithValue("@TotalEpisodes", subject.TotalEpisodes);
                command.Parameters.AddWithValue("@Eps", subject.Eps);
                command.Parameters.AddWithValue("@Type", subject.Type);
                command.Parameters.AddWithValue("@RatingRank", subject.Rating?.Rank   ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RatingTotal", subject.Rating?.Total ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RatingScore", subject.Rating?.Score ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ImagesSmall", subject.Images?.Small);
                command.Parameters.AddWithValue("@ImagesGrid", subject.Images?.Grid);
                command.Parameters.AddWithValue("@ImagesLarge", subject.Images?.Large);
                command.Parameters.AddWithValue("@ImagesMedium", subject.Images?.Medium);
                command.Parameters.AddWithValue("@ImagesCommon", subject.Images?.Common);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void DeleteBangumiApiSubjectJson(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            const string deleteSubject = "DELETE FROM BangumiApiSubjectJson WHERE Id = @Id";
            using (var command = new SQLiteCommand(deleteSubject, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            const string deleteTags = "DELETE FROM Tag WHERE BangumiApiSubjectJsonId = @BangumiApiSubjectJsonId";
            using (var command = new SQLiteCommand(deleteTags, connection))
            {
                command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", id);
                command.ExecuteNonQuery();
            }

            const string deleteInfoBoxes =
                "DELETE FROM InfoBox WHERE BangumiApiSubjectJsonId = @BangumiApiSubjectJsonId";
            using (var command = new SQLiteCommand(deleteInfoBoxes, connection))
            {
                command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", id);
                command.ExecuteNonQuery();
            }

            const string deleteRatingCounts =
                "DELETE FROM RatingCount WHERE BangumiApiSubjectJsonId = @BangumiApiSubjectJsonId";
            using (var command = new SQLiteCommand(deleteRatingCounts, connection))
            {
                command.Parameters.AddWithValue("@BangumiApiSubjectJsonId", id);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}