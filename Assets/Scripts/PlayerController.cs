using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General setup settings")]
    [Tooltip("How fast the ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 0;
    [Tooltip("How far the ship move horizontally")]
    [SerializeField] float xRange = 0;
    [Tooltip("How far the ship move vertically")]
    [SerializeField] float yRange = 0;

    [Header("Lasers array:")]
    [Tooltip("add lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("position based tuning")]
    [SerializeField] float positionPitchFactor=-2f;
    [SerializeField] float positionYawFactor=2f;

    [Header("control based tuning")]
    [SerializeField] float controlPitchFactor=-15f;
    [SerializeField] float controlRollFactor=-20f;

    float xThrow;
    float yThrow;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Quit();
        ProcessTranslation();
        ProcessRotation();
        ProccesFiring();
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow*controlPitchFactor;
        float yaw = transform.localPosition.x*positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXpos = transform.localPosition.x + xOffset;

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYpos = transform.localPosition.y + yOffset;

        float clampedXpos = Mathf.Clamp(newXpos, -xRange, xRange);
        float clampedYpos = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z);
    }

    void ProccesFiring()
    {
        if (Input.GetButton("Fire1"))
            ProccesShooting(true);
        else ProccesShooting(false);
    }

    private void ProccesShooting(bool isShooting)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isShooting;
        }
    }

    private void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
