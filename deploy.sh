cd ~/Events/EventsBookingTest
export PAYME_LOGIN=$1
export PAYME_PASSWORD=$2
export TELEGRAM_WEBHOOK=$3
export TELEGRAM_API=$4
git pull origin main
docker compose build
docker compose up -d