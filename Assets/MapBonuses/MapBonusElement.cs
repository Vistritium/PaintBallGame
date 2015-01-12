using AssemblyCSharp;
using Assets.Communication;
using UnityEngine;

namespace Assets.MapBonus
{
    public class MapBonusElement : MonoBehaviour
    {
        private int id;

        private Output output;
        private PlayerManager playerManager;
        private float timeLeft;
        private bool eaten = false;

        private void Start()
        {
            playerManager = GameObject.Find("Systems").GetComponent<PlayerManager>();
            output = GameObject.Find("Systems").GetComponent<Output>();
        }

        public void Initialize(int id, float lifeTime = -1.0f)
        {
            this.timeLeft = lifeTime;
            this.id = id;
        }

        private void Update()
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    Destroy(this.gameObject);
                }
            }

            if (!eaten)
            {
                foreach (var playerEntry in playerManager.players)
                {
                    GameObject player = playerEntry.Value;
                    if (
                        player.GetComponent<Player>()
                            .CollidesWithCircle(new Vector2(transform.position.x, transform.position.y),
                                transform.localScale.x * 0.3f))
                    {
                        //  Destroy(this.gameObject);
                        output.AteBonus(id);
                    }
                }  
            }

        }
    }
}