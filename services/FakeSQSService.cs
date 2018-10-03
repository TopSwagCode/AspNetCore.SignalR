using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace TopSwagCode.SignalR.services
{
    public class FakeSqsService : IAmazonSQS
    {
        private static Random rnd;

        public FakeSqsService()
        {
            if (rnd == null)
            {
                rnd = new Random();
            }
        }
        public void Dispose()
        {
            
        }

        public Task<Dictionary<string, string>> GetAttributesAsync(string queueUrl)
        {
            throw new NotImplementedException();
        }

        public Task SetAttributesAsync(string queueUrl, Dictionary<string, string> attributes)
        {
            throw new NotImplementedException();
        }

        public IClientConfig Config { get; }
        public Task<string> AuthorizeS3ToSendMessageAsync(string queueUrl, string bucket)
        {
            throw new NotImplementedException();
        }

        public Task<AddPermissionResponse> AddPermissionAsync(string queueUrl, string label, List<string> awsAccountIds, List<string> actions,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<AddPermissionResponse> AddPermissionAsync(AddPermissionRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChangeMessageVisibilityResponse> ChangeMessageVisibilityAsync(string queueUrl, string receiptHandle, int visibilityTimeout,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChangeMessageVisibilityResponse> ChangeMessageVisibilityAsync(ChangeMessageVisibilityRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChangeMessageVisibilityBatchResponse> ChangeMessageVisibilityBatchAsync(string queueUrl, List<ChangeMessageVisibilityBatchRequestEntry> entries,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ChangeMessageVisibilityBatchResponse> ChangeMessageVisibilityBatchAsync(ChangeMessageVisibilityBatchRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<CreateQueueResponse> CreateQueueAsync(string queueName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<CreateQueueResponse> CreateQueueAsync(CreateQueueRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DeleteMessageResponse> DeleteMessageAsync(string queueUrl, string receiptHandle,
            CancellationToken cancellationToken = new CancellationToken())
        {
            // All is fine and dandy :D
            return new Task<DeleteMessageResponse>(()=> new DeleteMessageResponse());
        }

        public async Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Delay(1);

            return new DeleteMessageResponse();
        }

        public Task<DeleteMessageBatchResponse> DeleteMessageBatchAsync(string queueUrl, List<DeleteMessageBatchRequestEntry> entries,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DeleteMessageBatchResponse> DeleteMessageBatchAsync(DeleteMessageBatchRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DeleteQueueResponse> DeleteQueueAsync(DeleteQueueRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<GetQueueAttributesResponse> GetQueueAttributesAsync(string queueUrl, List<string> attributeNames,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<GetQueueAttributesResponse> GetQueueAttributesAsync(GetQueueAttributesRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<GetQueueUrlResponse> GetQueueUrlAsync(string queueName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<GetQueueUrlResponse> GetQueueUrlAsync(GetQueueUrlRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ListDeadLetterSourceQueuesResponse> ListDeadLetterSourceQueuesAsync(ListDeadLetterSourceQueuesRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ListQueuesResponse> ListQueuesAsync(string queueNamePrefix, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ListQueuesResponse> ListQueuesAsync(ListQueuesRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ListQueueTagsResponse> ListQueueTagsAsync(ListQueueTagsRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<PurgeQueueResponse> PurgeQueueAsync(string queueUrl, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<PurgeQueueResponse> PurgeQueueAsync(PurgeQueueRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public async Task<ReceiveMessageResponse> ReceiveMessageAsync(string queueUrl, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Delay(1);
            return new ReceiveMessageResponse()
            {
                Messages = new List<Message>()
                {
                    new Message()
                    {
                        ReceiptHandle = "SomeHandle",
                        Body = "{'Message': '" + rnd.Next(0, 200) + "'}"
                    }
                }
            };
        }

        public Task<ReceiveMessageResponse> ReceiveMessageAsync(ReceiveMessageRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<RemovePermissionResponse> RemovePermissionAsync(string queueUrl, string label,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<RemovePermissionResponse> RemovePermissionAsync(RemovePermissionRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SendMessageResponse> SendMessageAsync(string queueUrl, string messageBody,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SendMessageResponse> SendMessageAsync(SendMessageRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SendMessageBatchResponse> SendMessageBatchAsync(string queueUrl, List<SendMessageBatchRequestEntry> entries,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SendMessageBatchResponse> SendMessageBatchAsync(SendMessageBatchRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SetQueueAttributesResponse> SetQueueAttributesAsync(string queueUrl, Dictionary<string, string> attributes,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<SetQueueAttributesResponse> SetQueueAttributesAsync(SetQueueAttributesRequest request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<TagQueueResponse> TagQueueAsync(TagQueueRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UntagQueueResponse> UntagQueueAsync(UntagQueueRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
