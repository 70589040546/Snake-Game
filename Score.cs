using System;
using System.IO;
using System.Text;

public class Score
{
  static readonly string scorePath= "score.txt";
  public static Score instance;
  public int score;
  private int _maxScore {get; set;}
  public Score(){
    if(instance == null){
        instance = this;
    }
    score = 0;
    ReadScoreFile();
  }
  public void ReadScoreFile(){
    if (File.Exists(scorePath)) {
        string scoreText = File.ReadAllText(scorePath);
        _maxScore = Convert.ToInt16(scoreText);
   }
   else{
    using (StreamWriter sw = File.CreateText("score.txt"))    
    {    
        sw.WriteLine("0"); 
    }      
   }

  }
  public int GetMaxScore(){
    return _maxScore;
  }
  public void CheckMaxAndCurrentScore(){
    _maxScore = (score > _maxScore) ? score : _maxScore;
    Console.Write(_maxScore);
  }
  
  // Fix this function => Open score file and delete first line and again write Max Score.
//   public void writeNewHighScore(){
//     string scoreLine;
//     StreamWriter sr = new StreamWriter("score.txt");
//     while((scoreLine = sr.ReadLine()) != null)
//     {
//         sw.WriteLine("");
//     } 
//   }
  
}