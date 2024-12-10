using System;

namespace SudokuGame
{
    class Program
    {
        static int[,] board = new int[9, 9];
        static bool[,] fixedNumbers = new bool[9, 9]; 

        static void Main(string[] args)
        {
            GenerateSudoku();
            PlayGame();
        }

        static void GenerateSudoku()
        {
            int[,] exampleBoard = {
                {5, 3, 0, 0, 7, 0, 0, 0, 0},
                {6, 0, 0, 1, 9, 5, 0, 0, 0},
                {0, 9, 8, 0, 0, 0, 0, 6, 0},
                {8, 0, 0, 0, 6, 0, 0, 0, 3},
                {4, 0, 0, 8, 0, 3, 0, 0, 1},
                {7, 0, 0, 0, 2, 0, 0, 0, 6},
                {0, 6, 0, 0, 0, 0, 2, 8, 0},
                {0, 0, 0, 4, 1, 9, 0, 0, 5},
                {0, 0, 0, 0, 8, 0, 0, 7, 9}
            };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = exampleBoard[i, j];
                    fixedNumbers[i, j] = (board[i, j] != 0);
                }
            }
        }

        static void PlayGame()
        {
            while (true)
            {
                Console.Clear();
                DisplayBoard();

                Console.WriteLine("Введите координаты (строка и столбец от 1 до 9) и число (0-9 для удаления): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") break;

                var parts = input.Split(' ');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int row) &&
                    int.TryParse(parts[1], out int col) &&
                    int.TryParse(parts[2], out int number))
                {
                    row -= 1; 
                    col -= 1; 

                    if (row >= 0 && row < 9 && col >= 0 && col < 9)
                    {
                        if (!fixedNumbers[row, col])
                        {
                            if (number == 0)
                            {
                                board[row, col] = 0;
                            }
                            else
                            {
                                board[row, col] = number;
                            }
                            if (CheckVictory())
                            {
                                Console.WriteLine("Поздравляем! Вы выиграли!");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Нельзя изменить фиксированное число! Нажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                        }
                    }
                }
            }
            static bool CheckVictory()
            {
                // Проверка всех строк
                for (int i = 0; i < 9; i++)
                {
                    bool[] seen = new bool[9];
                    for (int j = 0; j < 9; j++)
                    {
                        if (board[i, j] != 0)
                        {
                            if (seen[board[i, j] - 1]) return false; 
                            seen[board[i, j] - 1] = true; 
                        }
                    }
                }

                // Проверка всех столбцов
                for (int j = 0; j < 9; j++)
                {
                    bool[] seen = new bool[9];
                    for (int i = 0; i < 9; i++)
                    {
                        if (board[i, j] != 0)
                        {
                            if (seen[board[i, j] - 1]) return false;
                            seen[board[i, j] - 1] = true;
                        }
                    }
                }

                for (int boxRow = 0; boxRow < 3; boxRow++)
                {
                    for (int boxCol = 0; boxCol < 3; boxCol ++)
                    {
                        bool[] seen = new bool[9];
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                int row = boxRow * 3 + i;
                                int col = boxCol * 3 + j;
                                if (board[row, col] != 0)
                                {
                                    if (seen[board[row, col] - 1]) return false;
                                    seen[board[row, col] - 1] = true;
                                }
                            }
                        }
                    }
                }
                return true;
            }
        }

        static void DisplayBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0) Console.WriteLine("---------------------");
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0) Console.Write("|");
                    if (board[i, j] == 0)
                        Console.Write(" X "); 
                    else
                        Console.Write($" {board[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }

}
