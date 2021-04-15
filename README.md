# Nano35.Storage

    docker build -t bushemi2021/nano35.storage.api:latest ./Nano35.Storage.Api
    docker push bushemi2021/nano35.storage.api:latest
    docker build -t bushemi2021/nano35.storage.processor:latest ./Nano35.Storage.Processor
    docker push bushemi2021/nano35.storage.processor:latest
    docker build -t bushemi2021/nano35.storage.projection:latest ./Nano35.Storage.Projection
    docker push bushemi2021/nano35.storage.projection:latest
 