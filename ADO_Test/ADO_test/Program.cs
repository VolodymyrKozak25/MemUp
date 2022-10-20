using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Npgsql;

namespace ADO_test
{

    enum TableName
    {
        USERS,
        COLLECTIONS,
        MEMS
    }

    class Program
    {
        const string connString = "Host=localhost;Username=postgres;Password=1234;Database=MemUpDB";
        const TableName tableName = TableName.USERS;
        const uint INSERT_ROWS_COUNT = 40;

        static void Main(string[] args)
        {
            var time = Stopwatch.StartNew();

            Console.WriteLine("Inserting into table...");
            FillTableRandomly(tableName, INSERT_ROWS_COUNT).Wait();
            Console.WriteLine("Success!");

            DisplayTable(tableName).Wait();

            time.Stop();
            Console.WriteLine($"\nElapsed query execution time: {time.ElapsedMilliseconds}ms.");
        }

        static string GenerateName(int minSize = 4, int maxSize = 16)
        {
            const int ASCII_CODE_a = 97;
            const int ASCII_CODE_z = 122;

            Random rand = new Random();

            var nameLength = rand.Next(minSize-1, maxSize);
            var nextLetter = rand.Next(ASCII_CODE_a, ASCII_CODE_z+1);

            string gerenatedName = char.ToUpper((char)nextLetter).ToString();
            for (var i = 0; i < nameLength; i++)
            {
                nextLetter = rand.Next(ASCII_CODE_a, ASCII_CODE_z+1);
                gerenatedName += (char)nextLetter;
            }

            return "'" + gerenatedName + "'";
        }
        static int GeneratePositiveInt(int rangeStart, int rangeEnd)
        {
            Random rand = new Random();
            return rand.Next(rangeStart, rangeEnd);
        }
        static string InsertUsersQuery(uint entriesCount)
        {
            var insertQuery = "INSERT INTO users (user_name, MP_balance, day_streak, messages_status) VALUES ";
            for (var i = 0; i < entriesCount; i++)
            {
                if (i == entriesCount - 1)
                {
                    insertQuery += $"({GenerateName()}, {GeneratePositiveInt(0, 1000)}, " +
                                    $"{GeneratePositiveInt(0, 31)}, {Convert.ToBoolean(GeneratePositiveInt(0, 2))});";
                    continue;
                }
                insertQuery += $"({GenerateName()}, {GeneratePositiveInt(0, 1000)}, " +
                                $"{GeneratePositiveInt(0, 31)}, {Convert.ToBoolean(GeneratePositiveInt(0, 2))}), ";
            }
            return insertQuery;
        }
        static string InsertCollectionsQuery(uint entriesCount)
        {
            var insertQuery = "INSERT INTO collections (collection_name, daily_limit) VALUES ";
            for (var i = 0; i < entriesCount; i++)
            {
                if (i == entriesCount-1)
                {
                    insertQuery += $"({GenerateName(1, 62)}, {GeneratePositiveInt(0, 101)});";
                    continue;
                }
                insertQuery += $"({GenerateName(1, 62)}, {GeneratePositiveInt(0, 101)}), ";
            }
            return insertQuery;
        }
        static string InsertMemsQuery(uint entriesCount)
        {
            var insertQuery = "INSERT INTO mems (user_id, collection_id, " +
                                "question_text, answer_text, review_time) VALUES ";
            for (var i = 0; i < entriesCount; i++)
            {
                if (i == entriesCount - 1)
                {
                    insertQuery += $"({i + 1}, {entriesCount - i}, {GenerateName(10, 20)}, {GenerateName(10, 20)}, NOW());";
                    continue;
                }
                insertQuery += $"({i + 1}, {entriesCount - i}, {GenerateName(10, 20)}, {GenerateName(10, 20)}, NOW() + INTERVAL '{i} hours'), ";
            }
            return insertQuery;
        }
        static async Task FillTableRandomly(TableName tableName, uint entriesCount)
        {
            await using (var conn = new NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
                switch (tableName)
                {
                    case TableName.USERS:
                        await using (var insertEntries = new NpgsqlCommand(InsertUsersQuery(entriesCount), conn))
                        {
                            await insertEntries.ExecuteNonQueryAsync();
                        }
                        break;

                    case TableName.COLLECTIONS:
                        await using (var insertEntries = new NpgsqlCommand(InsertCollectionsQuery(entriesCount), conn))
                        {
                            await insertEntries.ExecuteNonQueryAsync();
                        }
                        break;

                    case TableName.MEMS:
                        await using (var insertEntries = new NpgsqlCommand(InsertMemsQuery(entriesCount), conn))
                        {
                            await insertEntries.ExecuteNonQueryAsync();
                        }
                        break;
                }
            }
        }

        static async Task DisplayTable(TableName tableName)
        {
            await using (var conn = new NpgsqlConnection(connString))
            {
                await conn.OpenAsync();
                switch (tableName)
                {
                    case TableName.USERS:
                        await ShowUsersTable(conn);
                        break;

                    case TableName.COLLECTIONS:
                        await ShowCollectionsTable(conn);
                        break;

                    case TableName.MEMS:
                        await ShowMemsTable(conn);
                        break;
                }
            }
        }
        static async Task ShowMemsTable(NpgsqlConnection conn)
        {
            await using (var selectCommand = new NpgsqlCommand("SELECT mem_id, user_id, collection_id," +
                                                               "question_text, answer_text, review_time FROM mems", conn))
            {
                await using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    Console.WriteLine(reader.GetName(0) + "\t" + reader.GetName(1) + "\t\t" +
                                      reader.GetName(2) + "\t" + reader.GetName(3) + "\t" +
                                      reader.GetName(4) + "\t" + reader.GetName(5));

                    while (await reader.ReadAsync())
                    {
                        var displayNameLengths = new int[] { Math.Min(reader.GetString(3).Length, 15), Math.Min(reader.GetString(4).Length, 15) };
                        Console.WriteLine(reader.GetInt32(0) + "\t\t" + reader.GetInt32(1) + "\t\t" +
                                          reader.GetInt32(2) + "\t" + reader.GetString(3).Substring(0, displayNameLengths[0]) + "\t" +
                                          reader.GetString(4).Substring(0, displayNameLengths[1]) + "\t" + reader.GetDateTime(5));
                    }
                }
            }
        }
        static async Task ShowCollectionsTable(NpgsqlConnection conn)
        {
            await using (var selectCommand = new NpgsqlCommand("SELECT * FROM collections", conn))
            {
                await using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    Console.WriteLine(reader.GetName(0) + "\t" + reader.GetName(1) + "\t\t" + reader.GetName(2));

                    while (await reader.ReadAsync())
                    {
                        var displayNameLength = Math.Min(reader.GetString(1).Length, 8);
                        if (displayNameLength > 4)
                        {
                            Console.WriteLine(reader.GetInt32(0) + "\t\t" + reader.GetString(1).Substring(0, displayNameLength) + "\t\t" +
                                          reader.GetInt32(2));
                        }
                        else
                        {
                            Console.WriteLine(reader.GetInt32(0) + "\t\t" + reader.GetString(1).Substring(0, displayNameLength) + "\t\t\t" +
                                          reader.GetInt32(2));
                        }
                    }
                }
            }
        }
        static async Task ShowUsersTable(NpgsqlConnection conn)
        {
            await using (var selectCommand = new NpgsqlCommand("SELECT * FROM users", conn))
            {
                await using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    Console.WriteLine(reader.GetName(0) + "\t\t" + reader.GetName(1) + "\t\t" +
                                      reader.GetName(2) + "\t" + reader.GetName(3) + "\t" +
                                      reader.GetName(4));

                    while (await reader.ReadAsync())
                    {
                        if (reader.GetString(1).Length < 8)
                        {
                            Console.WriteLine(reader.GetInt32(0) + "\t\t" + reader.GetString(1) + "\t\t\t" +
                                          reader.GetInt32(2) + "\t\t\t" + reader.GetInt32(3) + "\t\t" +
                                          reader.GetBoolean(4));
                        }
                        else if (reader.GetString(1).Length == 16)
                        {
                            Console.WriteLine(reader.GetInt32(0) + "\t\t" + reader.GetString(1) + "\t" +
                                          reader.GetInt32(2) + "\t\t\t" + reader.GetInt32(3) + "\t\t" +
                                          reader.GetBoolean(4));
                        }
                        else
                        {
                        Console.WriteLine(reader.GetInt32(0) + "\t\t" + reader.GetString(1) + "\t\t" +
                                          reader.GetInt32(2) + "\t\t\t" + reader.GetInt32(3) + "\t\t" +
                                          reader.GetBoolean(4));
                        }
                    }
                }
            }
        }

    }
}
