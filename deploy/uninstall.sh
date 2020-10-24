echo "To uninstall the services, pick one of the 3 options;"
echo "x - All services"
echo "b - Base services (database, service registry, elk)"
echo "a - Core services (file-store, gateway, security)"
read option

case $option in
"x")
  kubectl delete po,svc,deploy,ns -l app=devkit-app --namespace devkit-api-ns
  kubectl delete po,svc,deploy,ns -l app=devkit-app --namespace devkit-base-ns
  ;;
"b")
  kubectl delete po,svc,deploy,ns -l app=devkit-app --namespace devkit-base-ns
  ;;
"a")
  kubectl delete po,svc,deploy,ns -l app=devkit-app --namespace devkit-api-ns
  ;;
"x")
  echo "Invalid input!"
  ;;
esac