puts "Hello, Ruby!"

class HelloWorld
  def initialize(name)
    @name = name
  end

  def say_hello
    puts "Hello, #{@name}!"
  end
end

# Instantiate the HelloWorld class and call the say_hello method

hello = HelloWorld.new("Ruby")
hello.say_hello