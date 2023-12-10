## RabbitMQ

+ Các loại Exchange trong RabbitMQ phân biệt chúng qua cách chúng định tuyến (routing)
tin nhắn tới các hàng đợi. Dưới đây là sự phân biệt giữa một số loại Exchange phổ biến:

### 1. Direct Exchange
```
+ Định tuyến tin nhắn dựa trên một routing key cụ thể.
+ Chỉ tin nhắn có routing key khớp với routing key của hàng đợi mới được đưa vào hàng đợi đó.
+ Phù hợp cho các tình huống yêu cầu định tuyến chặt chẽ theo routing key.

```

### 2. Direct Exchange
```
+ Định tuyến tin nhắn tới tất cả các hàng đợi đã đăng ký với Exchange, bất kể routing key.
+ Phát sóng tin nhắn ra nhiều nơi (broadcast)
+ Thích hợp cho việc phân phối tin nhắn đến tất cả các ứng dụng đăng ký.
```

### 3. Topic Exchange
```
+ Định tuyến tin nhắn dựa trên mẫu (pattern) trong routing key.
+ Cung cấp khả năng định tuyến linh hoạt hơn, với wildcard characters (* và #).
+ Cho phép định tuyến dựa trên mẫu chuỗi trong routing key
```

### 4. Headers Exchange
```
+ Định tuyến dựa trên các thuộc tính (headers) của tin nhắn thay vì routing key.
+ Các headers được chỉ định khi tin nhắn được xuất bản, và hàng đợi chỉ nhận tin nhắn nếu các headers khớp.
```

### 5. Default Exchange (Direct Exchange không tên)
```
+ Exchange ẩn không cần khai báo, mọi hàng đợi có tên giống như routing key đều được liên kết với nó.
+ Gửi tin nhắn với routing key là tên của hàng đợi để đưa vào hàng đợi đó.
```

+ Mỗi loại Exchange có ưu điểm và ứng dụng cụ thể của mình, tùy thuộc vào yêu cầu cụ thể của hệ thống và cách bạn muốn định tuyến tin nhắn.