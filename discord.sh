#!/bin/bash
discord_url="https://discord.com/api/webhooks/1068873711207333929/84Ia6BVigTCcJME5KQxmmQJAOrnnSROtH--BWw3ehAifxJF1ZHwg1b9I3gGA1AGf8qSY"

generate_post_data() {
  cat <<EOF
{
  "content": "FoodpoolBackend Build ✅",
}
EOF
}


# POST request to Discord Webhook
curl -H "Content-Type: application/json" -X POST -d "$(generate_post_data)" $discord_url
 