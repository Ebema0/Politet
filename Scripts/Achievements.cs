using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{

    public enum ACHIEVEMENT_ID {
        COIN_COLLECTOR,
        TERMINATOR,
        MUSHROOM_COLLECTOR

    }

    public class Achievement {

        public ACHIEVEMENT_ID ID {get; private set;}
        public string Description {get; private set;}
        
        public bool Unlocked {get;private set;}

        public Achievement(ACHIEVEMENT_ID id, string desc){
            ID = id;
            Description = desc;
            Unlocked = false;
        }

        public void Unlock(){
            this.Unlocked = true;
        }

    }

    private List<Achievement> AllAchievements = new List<Achievement>();

    public GameObject player;
    private PlayerController player_controller;

    public void PrintUnlocked(){
        foreach (Achievement a in AllAchievements){
            if (a.Unlocked){
                Debug.Log(a.ID + ": " + a.Description);
            }
        }    
    }

    private bool Add(Achievement a){

        if (AllAchievements.Count < 1){
            AllAchievements.Add(a);
            Debug.Log("Achievement added: " + a.ID + ", " + a.Description);
            return true;
        }

        if (this.Contains(a)){
            Debug.Log("Achievement list already contains " + a.ID);
            return false;
        }
        else {
            AllAchievements.Add(a);
            Debug.Log("Achievement added: " + a.ID + ", " + a.Description);
            return true;
        }
    }

    private bool Contains(Achievement ach){
        foreach (Achievement a in AllAchievements){
            if (a.ID == ach.ID){
                return true;
            }
        }    

        return false;
    }

    private bool IsUnlocked(ACHIEVEMENT_ID id){
        foreach (Achievement a in AllAchievements){
            if (a.ID == id && a.Unlocked){
                return true;
            }
        }    
        return false;
    }

    private void Unlock(ACHIEVEMENT_ID id){
        foreach (Achievement a in AllAchievements){
            if (a.ID == id){
                a.Unlock();
                return;
            }
        }    
    }

    private void CheckCoins(Coin coin){

        if (player_controller.Coins == 5){
            Debug.Log("Achievement " + ACHIEVEMENT_ID.COIN_COLLECTOR + " unlocked!");
            // Set as achieved
            this.Unlock(ACHIEVEMENT_ID.COIN_COLLECTOR);
        }
    }

    private void CheckMushrooms(Mushroom mushroom)
    {

        if (player_controller.Mushrooms == 5)
        {
            Debug.Log("Achievement " + ACHIEVEMENT_ID.MUSHROOM_COLLECTOR + " unlocked!");
            // Set as achieved
            this.Unlock(ACHIEVEMENT_ID.MUSHROOM_COLLECTOR);
        }
    }

    private void CheckKills(Enemy enemy){
        if (player_controller.Kills == 10){
            Debug.Log("Achievement " + ACHIEVEMENT_ID.TERMINATOR  + " unlocked!");
            // Set as achieved
            this.Unlock(ACHIEVEMENT_ID.TERMINATOR);
        }
    }

    private void Start() {

        player_controller = player.GetComponent<PlayerController>();

        this.Add(new Achievement(ACHIEVEMENT_ID.COIN_COLLECTOR, "Collected 5 coins in the game."));
        Coin.OnCoinCollected += CheckCoins;

        this.Add(new Achievement(ACHIEVEMENT_ID.TERMINATOR, "Killed 10 enemies in the game."));
        Enemy.OnEnemyDie += CheckKills;

        this.Add(new Achievement(ACHIEVEMENT_ID.MUSHROOM_COLLECTOR, "Collected 1 Mushroom in the game."));
        Mushroom.OnMushroomCollected += CheckMushrooms;
       




    }

}
