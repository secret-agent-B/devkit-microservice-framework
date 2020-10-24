echo "To uninstall the services, pick one of the 3 options;"
echo "x - All services"
echo "b - Base services (database, service registry, elk)"
echo "a - Core services (file-store, gateway, security)"
read option

case $option in
"x")
  ./install-base.sh
  ./install-api.sh
  ;;
"b")
  ./install-base.sh
  ;;
"a")
  ./install-api.sh
  ;;
"x")
  echo "Invalid input!"
  ;;
esac