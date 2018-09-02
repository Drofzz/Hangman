using System;
using System.Linq;
using System.Collections.Generic;

public class Game
{
    /// <summary>
    /// The secret word
    /// </summary>
    private readonly string _word;
    /// <summary>
    /// A buffer of all the characters used in the word, this is neat to remove them, as they are discovered, if the buffer is empty, the secret word is discovered
    /// </summary>
    private readonly HashSet<char> _characterBuffer;

    /// <summary>
    /// Generate a display of the word, and how much the player have guessed
    /// </summary>
    public string Display => string.Join(" ", _word.Select(c => _history.Contains(c) ? c : '_').ToArray());

    public int Tries {get; private set;} = 0;
    public int Fails {get; private set;} = 0;

    private readonly HashSet<char> _history = new HashSet<char>();

    public bool GameOver {get;private set;} = false;

    public Game(string word)
    {
        _word = word.ToUpper() ?? throw new Exception("word can not be null");
        //Creates a buffer with all characters used in the word 
        _characterBuffer = _word.GroupBy(c => c).Select(g => g.First()).ToHashSet();
    }

    public void Guess(char guess)
    {
        Tries++; //Count anything as a try
        var c = char.ToUpper(guess); //UpperCase the character to normalize the imput
        if (!char.IsLetterOrDigit(c)) throw new Exception("Character can only be Letter or Number"); //Only letters and numbers validation
        if (!_history.Add(c)) throw new Exception($"Characer '{c}' has already been guessed"); //Only charakters that has not yet been tried validation
        if (!_characterBuffer.Remove(c)) {Fails++;throw new Exception($"Word does not contain the Character '{c}'");} //if the character can be removed them the buffer, it means it was in the word, otherwise count as failed guess
        if(_characterBuffer.Count == 0) GameOver = true; //if buffer is empty, the game is over
    }

    private static readonly Random Random = new Random();
    public static Game Create(params string[] words){
        //Make sure the word suggested can be spelled with Letters and is not null or empty
        var validatedWords = words.Where(w => !string.IsNullOrEmpty(w) && w.All(char.IsLetterOrDigit)).ToList();

        //Select a random word from the validated list
        var word = validatedWords[Random.Next(validatedWords.Count)];

        return new Game(word);
    }
}