using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    [SerializeField] private GameObject punch;
    [SerializeField] private float slowDuration = 10f;
    [SerializeField] private float slowValue = 0.4f;
    [SerializeField] private float slowEnd = 0.001f;
    [SerializeField] private int delay = 2000;
    [Space]
    [SerializeField] private bool simpleSlow = true;
    [SerializeField] private bool slowMode = false;
    [SerializeField] private bool endMode = false;
    [SerializeField] private bool timeStop = false;

    public float SlowValue => slowValue;
    public bool SimpleSlow  { get => simpleSlow ; set  => simpleSlow = value; }

    private const float doubleClickTime = 0.2f;
    private float lastClickTime;

    private void Start()
    {
        timeStop = false;
    }

    private void Update()
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.0f, 0.02f);

        if (slowMode && !endMode && !timeStop)
        {
            Time.timeScale += (1f / slowDuration) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime += (0.02f / slowDuration) * Time.unscaledDeltaTime;
        }
        else if (!slowMode && !endMode && !timeStop)
        {
            Time.timeScale += (1f) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime += (0.02f) * Time.unscaledDeltaTime;
        }
        else if (slowMode && endMode && !timeStop)
        {
            Time.timeScale += (10f) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime += (0.02f) * Time.unscaledDeltaTime;
        }
        else if (!slowMode && !endMode && timeStop)
        {
            Time.timeScale = slowEnd;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        if (Input.GetMouseButtonDown(0) && simpleSlow == true)
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTime)
            {
                StartCoroutine(StartSlow());
                SlowMotion();
            }
            else 
            {
                SlowMotion();
            }

            lastClickTime = Time.time;
        }

        if (Input.GetMouseButton(0) && simpleSlow == true)
        {
            SlowMotion();
        }

        if(punch.GetComponent<Punch>().gotHit == true)
        {
            endMode = true;
            StartCoroutine(Repeat());
        }               
    }

    public void SlowMotion()
    {
        Time.timeScale = slowValue;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;        
    }

    public async void SlowFinish()
    {
        await Task.Delay(delay);
        simpleSlow = false;
        slowMode = false;
        endMode = false;
        timeStop = true;
    }
    
    IEnumerator StartSlow()
    {
        simpleSlow = false;
        slowMode = true;
        slowValue = 0.05f;
        yield return new WaitForSeconds(0.8f);
        simpleSlow = true;
        slowMode = false;
        slowValue = 0.4f;
    }

    IEnumerator Repeat()
    {
        yield return new WaitForSeconds(0.5f);
        endMode = false;
    }
}