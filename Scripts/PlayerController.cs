using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{



    // Command-class, see Game Programming Patterns
    public abstract class Command
    {

        static private float m_moveamount = 1.0f;
        static public float MoveAmount { get { return m_moveamount; } }
        public abstract void Execute(Transform tf);
        public abstract void Undo(Transform tf);
    }

    public class MoveForward : Command
    {
        public override void Execute(Transform tf)
        {
            tf.Translate(Vector3.forward * Command.MoveAmount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(-Vector3.forward * Command.MoveAmount);
        }
    }
    public class MoveBackward : Command
    {
        public override void Execute(Transform tf)
        {
            tf.Translate(-Vector3.forward * Command.MoveAmount);
        }

        public override void Undo(Transform tf)
        {
            tf.Translate(Vector3.forward * Command.MoveAmount);
        }

    }

    public class MoveLeft : Command
    {
        public override void Execute(Transform tf)
        {
            tf.Translate(-Vector3.right * Command.MoveAmount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(Vector3.right * Command.MoveAmount);
        }

    }

    public class MoveRight : Command
    {
        public override void Execute(Transform tf)
        {
            tf.Translate(Vector3.right * Command.MoveAmount);
        }
        public override void Undo(Transform tf)
        {
            tf.Translate(-Vector3.right * Command.MoveAmount);
        }

    }

    public class DoNothing : Command
    {
        public override void Execute(Transform tf) { }
        public override void Undo(Transform tf) { }
    }

    public float speed = 10f;

    private float orginalSpeed;

    public float scaleMultiplier = 1.5f;
    private Vector3 originalScale;
    public void IncreaseSpeed(float amount)

    {
        speed += amount;
    }
    
    public void ResetSpeed()
    {
        speed = orginalSpeed;
    }
    public void IncreaseScale()
    {
        transform.localScale = originalScale * scaleMultiplier;
    }


    public void ResetScale()
    {
        transform.localScale = originalScale;
    }
    public float MoveAmount = 1.0f;

    private Command button_W = new MoveForward();
    private Command button_A = new MoveLeft();
    private Command button_S = new MoveBackward();
    private Command button_D = new MoveRight();

    Stack<Command> undostack = new Stack<Command>();
    Stack<Command> redostack = new Stack<Command>();

    private Vector3 startpos;

    
    private bool IsReplaying = false;

    public MyEventSystem eventSystem; 

    
    public Enemy enemyPrefab;

    public Coin coinPrefab;

    public Mushroom mushroomPrefab;
  
    [SerializeField]
    private int Score = 0;
    [SerializeField]
    private int EnemiesKilled = 0;
    [SerializeField]
    private int Money = 0;
    [SerializeField]
    private int CoinsCollected = 0;
    [SerializeField]
    private int Ability = 0;
    [SerializeField]
    private int MushroomsCollected = 0;




    public int Coins => CoinsCollected;

    public int Mushrooms => MushroomsCollected;

    public int Kills => EnemiesKilled;

    void AddToScore(Enemy enemyscript)
    {
        Debug.Log("Enemy died!");
        this.Score += enemyscript.enemyValue;
        this.EnemiesKilled++;
        Debug.Log("You've killed " + EnemiesKilled + " enemies! Brutal!!!");
    }

    void AddToMoney(Coin coin)
    {
        Debug.Log("Coin collected!");
        this.Money += coin.coinValue;
        this.CoinsCollected++;
        Debug.Log("You now have " + Money + " money!");
    }

    void AddToAbility(Mushroom mushroom)
    {
        Debug.Log("Mushroom Collected");
        this.Ability += mushroom.mushroomValue;
        this.MushroomsCollected++;
        Debug.Log("You now have " + Ability + " Super Mushroom!");
    }


    private void StartReplay()
    {
        this.IsReplaying = true;

        // move transform to start position
        transform.position = startpos;

        Stack<Command> replaystack = new Stack<Command>();
        // get all commands from undo-stack -- in reverse order
        while (undostack.TryPop(out Command cmd))
        {
            replaystack.Push(cmd);
        }

        Command[] cmds = replaystack.ToArray();

        while (replaystack.TryPop(out Command cmd))
        {
            undostack.Push(cmd);
        }


        if (cmds.Length > 0)
        {
            // do the replay with co-routine
            Coroutine corReplay = StartCoroutine(Replay(transform, cmds));
        }
        else
        {
            this.IsReplaying = false;
        }

    }

    IEnumerator Replay(Transform tf, Command[] replaycommands)
    {
        for (int i = 0; i < replaycommands.Length; i++)
        {
            replaycommands[i].Execute(tf);
            yield return new WaitForSeconds(0.1f);
        }
        // ... remember to set IsReplaying to false once we are done.
        IsReplaying = false;
    }

    private void Awake()
    {
        
        Enemy.OnEnemyDie += AddToScore;

        Coin.OnCoinCollected += AddToMoney;

        Mushroom.OnMushroomCollected += AddToAbility;

       

    }

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        orginalSpeed = speed;
        


    }

    // Update is called once per frame
    void Update()
    {
        if (!IsReplaying)
        {
           

            if (Input.GetKeyDown(KeyCode.Space)) {
                GameObject go = Instantiate(enemyPrefab,
                                Random.insideUnitSphere,
                                enemyPrefab.transform.rotation).gameObject;

                Destroy(go, 5f);  
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                Coin coin = Instantiate(coinPrefab,
                                Random.insideUnitSphere,
                                coinPrefab.transform.rotation);

                coin.SetValue(100);


                Destroy(coin.gameObject, 5f);  
            }
              if (Input.GetKeyDown(KeyCode.I)) {
                Mushroom mushroom = Instantiate(mushroomPrefab,
                                Random.insideUnitSphere,
                                mushroomPrefab.transform.rotation);

                mushroom.SetValue(10);


                Destroy(mushroom.gameObject, 5f);  
            }
            


            // Just use the Commands - we don't need to know about 
            // 
            if (Input.GetKeyDown(KeyCode.W))
            {
                button_W.Execute(transform);
                undostack.Push(button_W);
                redostack.Clear();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                button_A.Execute(transform);
                undostack.Push(button_A);
                redostack.Clear();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                button_S.Execute(transform);
                undostack.Push(button_S);
                redostack.Clear();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                button_D.Execute(transform);
                undostack.Push(button_D);
                redostack.Clear();
            }
            if (Input.GetKeyDown(KeyCode.Z) && !Input.GetKey(KeyCode.LeftShift))
            {
                if (undostack.TryPop(out Command cmd))
                {
                    cmd.Undo(transform);
                    redostack.Push(cmd);
                }
            }
            if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftShift))
            {
                if (redostack.TryPop(out Command cmd))
                {
                    cmd.Execute(transform);
                    undostack.Push(cmd);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                SceneManager.LoadScene("MainMenu");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {

                StartReplay();

            

            }


        }

    }

}
