# clean up
kubectl delete po,svc,deploy,ns -l app=devkit-app --namespace devkit-base-ns

# create namespace
kubectl create -f base/devkit-base-ns.yaml

# create deployments
kubectl create -f base/consul-deploy.yaml
kubectl create -f base/elasticsearch-deploy.yaml
kubectl create -f base/kibana-deploy.yaml
kubectl create -f base/mongo-deploy.yaml

# create services
kubectl create -f base/consul-service.yaml
kubectl create -f base/elasticsearch-service.yaml
kubectl create -f base/kibana-service.yaml
kubectl create -f base/mongo-service.yaml