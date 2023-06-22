puts 'Hello, World!'
address = [1, 2, 3]
p address
p address[1]
new_address = address.reverse

puts 'Here new_address'
p new_address
puts 'Here address'
p address

new_address = address.reverse!

puts 'Here new_address'
p new_address
puts 'Here address'
p address
