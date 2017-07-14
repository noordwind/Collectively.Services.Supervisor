#!/bin/bash
DOCKER_ENV=''
DOCKER_TAG=''
case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_ENV=production
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_ENV=development
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -f ./src/Collectively.Services.Supervisor/Dockerfile.$DOCKER_ENV -t collectively.services.supervisor:$DOCKER_TAG ./src/Collectively.Services.Supervisor
docker tag collectively.services.supervisor:$DOCKER_TAG $DOCKER_USERNAME/collectively.services.supervisor:$DOCKER_TAG
docker push $DOCKER_USERNAME/collectively.services.supervisor:$DOCKER_TAG