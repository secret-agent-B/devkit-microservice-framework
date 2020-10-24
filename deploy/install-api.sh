# clean up
kubectl delete po,svc,deploy,ns -l app=logistics-app --namespace logistics-api-ns

# create namespace
kubectl create -f apps/core/logistics-api-ns.yaml

# create deployments
kubectl create -f apps/core/file-store-deploy.yaml
kubectl create -f apps/core/gateway-deploy.yaml
kubectl create -f apps/core/security-deploy.yaml

# create services
kubectl create -f apps/core/file-store-service.yaml
kubectl create -f apps/core/gateway-service.yaml
kubectl create -f apps/core/security-service.yaml