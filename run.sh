#!/bin/bash
cp /home/bxdman/secret/* ./
if [ $( docker ps -a | grep foodpool_backend_api | wc -l ) -gt 0 ]; then
  docker-compose up -d --build
else
  docker-compose up -d
fi