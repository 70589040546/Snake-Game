using System;
using System.IO;
using System.Linq;
using System.Text;

public class Score
{
  public static readonly string scorePath= "score.txt";
  public static Score Instance;
  public int score{get;set;}
  private int _maxScore {get; set;}
  public Score(){
    if(Instance == null){
        Instance = this;
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
    if(score > _maxScore){
      _maxScore = score;
      writeNewHighScore();
    }
   
  }
  public void writeNewHighScore(){
    var lines = File.ReadAllLines("score.txt");
    File.WriteAllLines("score.txt", lines.Skip(1).ToArray());
    File.WriteAllText("score.txt",  _maxScore.ToString());
  }
  
}